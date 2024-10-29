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

namespace ExtractRunningData.Classes
{
    public static class ExcelFileHandler
    {
        public static List<EventData> ProcessExcelFile(string filePath)
        {
            // read data from D:\projects\ExtractRunningData\wavacalc15.xls
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return ReadEventDataFromExcel(stream);
            //PrintEventData(eventDataList);
        }

        public static List<EventData> ReadEventDataFromExcel(Stream stream)
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
    }
}
