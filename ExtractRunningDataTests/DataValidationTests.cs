using ExtractRunningData.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRunningDataTests
{
    internal class DataValidationTests : TestSetupBase
    {
        [Test, Description("Ensure all Excel data has been read")]
        public void HasAllExcelDataBeenRead()
        {
            // Assert: Verify the event data list has 146 entries
            Assert.That(_eventDataList, Has.Count.EqualTo(146));
        }

        [Test, Description("Verify all running data is present in the database")]
        public void HasAllRunningData()
        {
            //Arrange
            DatabaseHandler.SaveEventDataToDatabase( _context, _eventDataList);
            //Act
            var eventData = _context.EventData.ToList();
            // Assert: Verify the number of entries in the database is 146
            Assert.That(eventData, Has.Count.EqualTo(146));
        }
    }
}
