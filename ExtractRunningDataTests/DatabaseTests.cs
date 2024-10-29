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
        [Test, Description("Save event data to database")]
        public void CanSaveEventDataToDatabase()
        {
            // Arrange: Save event data to the database
            DatabaseHandler.SaveEventDataToDatabase(_context, _eventDataList);

            // Act: Retrieve the first event data entry from the database
            var eventData = _context.EventData.Find(1);

            // Assert: Verify the retrieved event data is not null
            Assert.That(eventData, Is.Not.Null);
        }

        public static bool DoesTableExist(DbContext context, string tableName)
        {
            return context.Database.GetDbConnection().GetSchema("Tables")
                .Rows.Cast<System.Data.DataRow>()
                .Any(row => row["TABLE_NAME"].ToString() == tableName);
        }
    }
}