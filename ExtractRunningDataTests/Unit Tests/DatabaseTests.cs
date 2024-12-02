using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractRunningData.Classes;
using Microsoft.EntityFrameworkCore;

namespace ExtractRunningDataTests.Unit
{
    /// <summary>
    /// Test class for database operations
    /// </summary>
    [TestFixture]
    internal class DatabaseTests : TestSetupBase
    {
        /// <summary>
        /// Setup the test environment
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // Arrange: Save event data to in memory database
            DatabaseHandler.SaveEventDataToDatabase(_context, _eventDataList);
        }

        /// <summary>
        /// Test to verify that event data is saved to the database
        /// </summary>
        [Test, Description("Save event data to database")]
        [Category("UnitTest")]
        public void CanSaveEventDataToDatabase()
        {
            // Arrange
            var eventData = _context.RunningEventData;

            //Act
            var firstRow = eventData.Find(1);

            // Assert: Verify the retrieved event data is not null
            Assert.That(firstRow, Is.Not.Null);
        }

        /// <summary>
        /// Test to verify that the first row in the Excel file is the same as the first row in the database
        /// </summary>
        [Test, Description("Check 1st excel row is the same as 1st database row")]
        [Category("UnitTest")]
        public void CheckFirstExcelRowIsSameAsFirstDatabaseRow()
        {
            // Arrange: Setup event data
            var eventData = _context.RunningEventData;

            // Act: Get first row
            var firstExcelRow = eventData.Find(1);
            var firstDbRow = _eventDataList[0];

            // Assert: Verify the first event data entry in the database matches the first row in the Excel file
            Assert.Multiple(() =>
            {
                Assert.That(firstExcelRow?.EventName, Is.EqualTo(firstDbRow.EventName), "Event name should match.");
                Assert.That(firstExcelRow?.RunningEventFactors.First().Age, Is.EqualTo(firstDbRow.RunningEventFactors.First().Age), "Age from first factors entry should match.");
                Assert.That(firstExcelRow?.RunningEventFactors.Last().Factor, Is.EqualTo(firstDbRow.RunningEventFactors.Last().Factor), "Factor from last factors entry should match.");
            });
        }

        /// <summary>
        /// Test to verify that the last row in the Excel file is the same as the last row in the database
        /// </summary>
        [Test, Description("Check last excel row is the same as last database row")]
        [Category("UnitTest")]
        public void CheckLastExcelRowIsSameAsLastDatabaseRow()
        {
            // Arrange
            var eventData = _context.RunningEventData;

            //Act: Get last row
            var lastExcelRow = eventData.Find(_eventDataList.Count);
            var LastDbRow = _eventDataList[^1];

            // Assert: Verify the last event data entry in the database matches the last row in the Excel file
            Assert.Multiple(() =>
            {
                Assert.That(lastExcelRow?.EventName, Is.EqualTo(LastDbRow.EventName), "Event name should match.");
                Assert.That(lastExcelRow?.RunningEventFactors.First().Age, Is.EqualTo(LastDbRow.RunningEventFactors.First().Age), "Age from first factors entry should match.");
                Assert.That(lastExcelRow?.RunningEventFactors.Last().Factor, Is.EqualTo(LastDbRow.RunningEventFactors.Last().Factor), "Factor from last factors entry should match.");
            });
        }
    }
}