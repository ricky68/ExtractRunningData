using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExcelDataReader;
using ExtractRunningData.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySqlConnector;

namespace ExtractRunningData.Classes
{
    /// <summary>
    /// ExcelFileHandler: Reads data from an Excel file and returns a list of RunningEventData objects.
    /// </summary>
    public static class ExcelFileHandler
    {
        /// <summary>
        /// ProcessExcelFile: Processes an Excel file and returns a list of RunningEventData objects.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>List<RunningEventData></returns>
        public static List<RunningEventData> ProcessExcelFile(string filePath)
        {
            // read data from D:\projects\ExtractRunningData\wavacalc15.xls
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return ReadEventDataFromExcel(stream);
            //PrintEventData(eventDataList);
        }

        /// <summary>
        /// ReadEventDataFromExcel: Process Stream and return a list of RunningEventData objects.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>List<RunningEventData></returns>
        public static List<RunningEventData> ReadEventDataFromExcel(Stream stream)
        {
            List<RunningEventData> eventDataList = [];
            List<RunningEventFactors> eventFactorList = [];
            int eventDataID = 1;
            int eventFactorsID = 1;

            using var reader = ExcelReaderFactory.CreateReader(stream);

            // "Women" and "Men" are the first two sheets in the Excel file
            while (reader.Name == "Women" || reader.Name == "Men")
            {
                //skip first row and add the data to our models
                reader.Read();
                while (reader.Read())
                {
                    var runningEventData = new Models.RunningEventData
                    {
                        RunningEventDataId = eventDataID++,
                        EventName = reader[1].ToString() ?? string.Empty,
                        IsRoad = Convert.ToBoolean(reader[2]),
                        Distance = Convert.ToDouble(reader[3]),
                        OC = Convert.ToDouble(reader[4]),
                        Sex = reader.Name == "Women" ? 'F' : 'M'
                    };

                    for (int i = 5; i <= 100; i++)
                    {
                        var eventFactors = new RunningEventFactors
                        {
                            RunningEventFactorsId = eventFactorsID++,
                            Age = i, //reader.GetInt32(i),
                            Factor = reader.GetDouble(i),
                            RunningEventDataId = runningEventData.RunningEventDataId,
                            RunningEventData = runningEventData
                        };
                        eventFactorList.Add(eventFactors);
                    }
                    runningEventData.RunningEventFactors = eventFactorList;
                    eventFactorList = [];
                    eventDataList.Add(runningEventData);
                }
                reader.NextResult();
            }

            return eventDataList;
        }

#if DEBUG
        /// <summary>
        /// PrintEventData: Not used but usefull for debugging. Not used in the final version.
        /// </summary>
        /// <param name="RunningEventDataList"></param>
        private static void PrintEventData(List<IRunningEventData> RunningEventDataList)
        {
            Console.WriteLine("Event Name   Is Road   Distance   OC   Age");

            foreach (var eventData in RunningEventDataList)
            {
                Console.Write($"{eventData.EventName,-10}");
                Console.Write($"{eventData.IsRoad,-6}");
                Console.Write($"{eventData.Distance,-10} km");
                Console.Write($"{eventData.OC,-9}");

                foreach (var factor in eventData.RunningEventFactors)
                {
                    Console.Write($"{factor.Factor,-8}");
                }
                Console.WriteLine();
            }
        }
#endif
    }
}
