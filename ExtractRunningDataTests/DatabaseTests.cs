using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractRunningData.Classes;
using Microsoft.EntityFrameworkCore;

namespace ExtractRunningDataTests
{
    internal class DatabaseTests : TestSetupBase
    {
        [SetUp]
        public void SetUp()
        {
            // Arrange: Save event data to the database
            DatabaseHandler.SaveEventDataToDatabase(_context, _eventDataList);
        }

        [Test, Description("Save event data to database")]
        public void CanSaveEventDataToDatabase()
        {
            // Act: Retrieve the first event data entry from the database
            var eventData = _context.RunningEventData;

            // Assert: Verify the retrieved event data is not null
            Assert.That(eventData.Find(1), Is.Not.Null);
        }

        [Test, Description("Check 1st excel row is the same as 1st database row")]
        public void CheckFirstExcelRowIsSameAsFirstDatabaseRow()
        {
            var eventData = _context.RunningEventData;
            var firstRow = eventData.Find(1);
            // Assert: Verify the first event data entry in the database matches the first row in the Excel file
            Assert.Multiple(() =>
            {
                Assert.That(firstRow?.EventName, Is.EqualTo(_eventDataList[0].EventName), "Event name should match.");
                Assert.That(firstRow?.RunningEventFactors.First().Age, Is.EqualTo(_eventDataList[0].RunningEventFactors.First().Age), "Age from first factors entry should match.");
                Assert.That(firstRow?.RunningEventFactors.Last().Factor, Is.EqualTo(_eventDataList[0].RunningEventFactors.Last().Factor), "Factor from last factors entry should match.");
            });
        }

        [Test, Description("Check last excel row is the same as last database row")]
        public void CheckLastExcelRowIsSameAsLastDatabaseRow()
        {
            var eventData = _context.RunningEventData;
            var lastRow = eventData.Find(_eventDataList.Count);
            // Assert: Verify the last event data entry in the database matches the last row in the Excel file
            Assert.Multiple(() =>
            {
                Assert.That(lastRow?.EventName, Is.EqualTo(_eventDataList[^1].EventName), "Event name should match.");
                Assert.That(lastRow?.RunningEventFactors.First().Age, Is.EqualTo(_eventDataList[^1].RunningEventFactors.First().Age), "Age from first factors entry should match.");
                Assert.That(lastRow?.RunningEventFactors.Last().Factor, Is.EqualTo(_eventDataList[^1].RunningEventFactors.Last().Factor), "Factor from last factors entry should match.");
            });
        }
    }
}