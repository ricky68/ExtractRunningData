using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExcelDataReader;
using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ExtractRunningData
{
    public class OpenExcelFile : IDisposable
    {
        public readonly EventDataContext _context;
        private bool disposedValue;

        public OpenExcelFile(EventDataContext context)
        {
            _context = context;
        }

        public void ProcessExcelFile(string filePath)
        {
            // read data from D:\projects\ExtractRunningData\wavacalc15.xls
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var eventDataList = ReadEventDataFromExcel(stream);
            //PrintEventData(eventDataList);
            SaveEventDataToDatabase(eventDataList);
        }

        public List<EventData> ReadEventDataFromExcel(Stream stream)
        {
            List<EventData> eventDataList = [];
            List<EventFactors> eventFactorList = [];
            int eventDataID = 1;
            int eventFactorsID = 1;

            using var reader = ExcelReaderFactory.CreateReader(stream);

            // "Women" and "Men" are the first two sheets in the Excel file
            while (reader.Name == "Women" || reader.Name == "Men")
            {
                //skip first row
                reader.Read();
                while (reader.Read())
                {
                    var eventData = new EventData
                    {
                        EventDataId = eventDataID++,
                        EventName = reader.GetString(1),
                        IsRoad = Convert.ToBoolean(reader.GetDouble(2)),
                        Distance = reader.GetDouble(3),
                        OC = reader.GetDouble(4),
                        Sex = reader.Name == "Women" ? 'F' : 'M'
                    };

                    for (int i = 5; i <= 100; i++)
                    {
                        var eventFactors = new EventFactors
                        {
                            EventFactorsId = eventFactorsID++,
                            Age = i, //reader.GetInt32(i),
                            Factor = reader.GetDouble(i),
                            EventDataId = eventData.EventDataId,
                            EventData = eventData
                            // Add more properties as needed
                        };
                        eventFactorList.Add(eventFactors);
                    }
                    eventData.EventFactors = eventFactorList;
                    eventFactorList = [];
                    eventDataList.Add(eventData);
                }
                reader.NextResult();
            }

            return eventDataList;
        }

        public bool DoesTableExist(DbContext context, string tableName)
        {
            return context.Database.GetDbConnection().GetSchema("Tables")
                .Rows.Cast<System.Data.DataRow>()
                .Any(row => row["TABLE_NAME"].ToString() == tableName);
        }

        public void SaveEventDataToDatabase(List<EventData> EventDataList)
        {
            try
            {
                // Our test database is in-memory, so we need to check if the table exists
                if (_context.Database.IsRelational())
                {
                    _context.Database.ExecuteSql($"DELETE FROM eventdata");
                    _context.Database.ExecuteSql($"DELETE FROM eventfactors");
                }
                _context.EventData.AddRange((IEnumerable<EventData>)EventDataList);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error saving data: {ex.InnerException?.Message}");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
            }
        }

        private static void PrintEventData(List<IEventData> EventDataList)
        {
            Console.WriteLine("Event Name   Is Road   Distance   OC   Age");

            foreach (var eventData in EventDataList)
            {
                Console.Write($"{eventData.EventName,-10}");
                Console.Write($"{eventData.IsRoad,-6}");
                Console.Write($"{eventData.Distance,-10} km");
                Console.Write($"{eventData.OC,-9}");

                foreach (var factor in eventData.EventFactors)
                {
                    Console.Write($"{factor.Factor,-8}");
                }
                Console.WriteLine();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~OpenExcelFile()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
