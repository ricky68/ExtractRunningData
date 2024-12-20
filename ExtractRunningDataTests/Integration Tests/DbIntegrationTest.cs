﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using ExtractRunningData;
using NUnit.Framework;
using System.IO;
using System;
using Microsoft.Extensions.Options;

namespace ExtractRunningDataTests.Integration
{
    /// <summary>
    /// Integration tests for the database
    /// </summary>
    [TestFixture]
    public class DbIntegrationTest
    {
        private static IConfigurationRoot? Configuration { get; set; }
        private static DbContextOptionsBuilder<EventDataContext> optionsBuilder = new();

        /// <summary>
        /// Setup the configuration and options for the database
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
 
            optionsBuilder.UseMySql(Configuration?.GetConnectionString("DefaultConnection")
                , new MySqlServerVersion(new Version(11, 5, 2)));
        }

        /// <summary>
        /// Check if the table exists in the database
        /// </summary>
        [Test, Description("Check Table Exists using real database from appsettings.json")]
        [Category("IntegrationTest")]
        public void CheckTableExists()
        {
            // Arrange
            using var context = new EventDataContext(optionsBuilder.Options);

            // Act
            bool tableDateExists = DoesTableExist(context, "runningeventdata");
            bool tableFactorsExists = DoesTableExist(context, "runningeventfactors");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(tableDateExists, Is.True, "The table should exist in the database.");
                Assert.That(tableFactorsExists, Is.True, "The table should exist in the database.");
            });
        }

        /// <summary>
        /// Check if the column exists in the table
        /// </summary>
        [Test, Description("Check Column Exists using real database from appsettings.json")]
        [Category("IntegrationTest")]
        public void CheckColumnExists()
        {
            // Arrange
            using var context = new EventDataContext(optionsBuilder.Options);

            // Act
            bool columnEventNameExists = DoesColumnExist(context, "runningeventdata", "EventName");
            bool columnAgeExists = DoesColumnExist(context, "runningeventfactors", "Age");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(columnEventNameExists, Is.True, "The column should exist in the table.");
                Assert.That(columnAgeExists, Is.True, "The column should exist in the table.");
            });
        }

        /// <summary>
        /// Check tables have data
        /// </summary>
        [Test, Description("Check tables have data using real database from appsettings.json")]
        [Category("IntegrationTest")]
        public void CheckTablesHaveData()
        {
            // Arrange
            using var context = new EventDataContext(optionsBuilder.Options);
            // Act
            var eventData = context.RunningEventData
                .Include(e => e.RunningEventFactors)
                .FirstOrDefault(e => e.RunningEventDataId == 1);

            var eventDataCount = context.RunningEventData.Count();
            var eventFactorsCount = eventData?.RunningEventFactors.Count;
            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(eventDataCount, Is.GreaterThan(0), "The table should have data.");
                Assert.That(eventFactorsCount, Is.GreaterThan(0), "The table should have data.");
            });
        }

        /// <summary>
        /// Check if the table exists in the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tableName"></param>
        /// <returns>bool</returns>
        public static bool DoesTableExist(DbContext context, string tableName)
        {

            var connection = context.Database.GetDbConnection();
            connection.Open();
            var tables = connection.GetSchema("Tables", [null, null, tableName, null]);
            connection.Close();

            return tables.Rows.Cast<DataRow>().Any(row => row["TABLE_NAME"].ToString() == tableName);
        }

        public static bool DoesColumnExist(DbContext context, string tableName, string columnName)
        {
            var connection = context.Database.GetDbConnection();
            connection.Open();
            var columns = connection.GetSchema("Columns", [null, null, tableName, null]);
            connection.Close();
            return columns.Rows.Cast<DataRow>().Any(row => row["COLUMN_NAME"].ToString() == columnName);
        }
    }
}