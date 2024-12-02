using ExtractRunningData.Classes;
using ExtractRunningData;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractRunningData.Models;
using Microsoft.Extensions.Configuration;

namespace ExtractRunningDataTests.Unit
{
    /// <summary>
    /// Base class for setting up the test environment
    /// </summary>
    [TestFixture]
    public abstract class TestSetupBase
    {
        protected FileStream? _stream;
        protected List<RunningEventData> _eventDataList = [];
        protected IConfigurationRoot _configuration;
        protected EventDataContext _context;

        /// <summary>
        /// Setup the test environment
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Arrange: Initialize the database context and file path
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EventDataContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb");
            _context = new EventDataContext(optionsBuilder.Options);
            var filePath = _configuration["ExcelFilePath"] ?? @"C:\projects\ExtractRunningData\ExtractRunningData\wavacalc15.xls";

            // Act: Open the Excel file and read event data
            _stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _eventDataList = ExcelFileHandler.ReadEventDataFromExcel(_stream);
        }

        /// <summary>
        /// Tear down the test environment
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Clean up resources
            _stream?.Dispose();
            _context?.Dispose();
        }
    }
}
