using NUnit.Framework;
using System;
using System.IO;
using ExtractRunningData;
using ExtractRunningData.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExtractRunningDataTests
{
    public class Tests
    {
        private OpenExcelFile _openExcelFile;
        private FileStream? _stream;
        private List<EventData> _eventDataList = [];
        [SetUp]
        public void Setup()
        {
            //Arrange
            var optionsBuilder = new DbContextOptionsBuilder<EventDataContext>();
            optionsBuilder.UseInMemoryDatabase("TestDb");
            var _context = new EventDataContext(optionsBuilder.Options);
            var filePath = @"D:\projects\ExtractRunningData\wavacalc15.xls";

            //Act
            _stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _openExcelFile = new OpenExcelFile(_context);
            _eventDataList = _openExcelFile.ReadEventDataFromExcel(_stream);
        }

        //create a test to check if the file can be opened @"D:\projects\ExtractRunningData\wavacalc15.xls"
        [Test]
        public void CanOpenFile()
        {
        // Assert
            Assert.That(_stream, Is.Not.Null);
        }

        [Test]
        public void CanReadEventDataFromExcel()
        {
            // Assert
            Assert.That(_eventDataList, Is.Not.Null);
        }

        [Test]
        public void HasAllExcelDataBeenRead()
        {
            // Assert
            Assert.That(_eventDataList, Has.Count.EqualTo(146));
        }

        [Test]
        public void CanSaveEventDataToDatabase()
        {
            //Arrange
            _openExcelFile.SaveEventDataToDatabase(_eventDataList);
            //Act
            var eventData = _openExcelFile._context.EventData.Find(1);
            //Assert
            Assert.That(eventData, Is.Not.Null);
        }

        [Test]
        public void HasAllRunningData()
        {
            //Arrange
            _openExcelFile.SaveEventDataToDatabase(_eventDataList);
            //Act
            var eventData = _openExcelFile._context.EventData.ToList();
            //Assert
            Assert.That(eventData, Has.Count.EqualTo(146));
        }

        [TearDown]
        public void TearDown()
        {
            _stream?.Dispose();
            _openExcelFile?.Dispose();
        }
    }
}