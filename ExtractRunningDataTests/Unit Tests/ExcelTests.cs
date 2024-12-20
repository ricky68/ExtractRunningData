﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRunningDataTests.Unit
{
    /// <summary>
    /// Unit tests for Excel file handling
    /// </summary>
    [TestFixture]
    internal class ExcelTests : TestSetupBase
    {
        /// <summary>
        /// Check if the file can be opened
        /// </summary>
        [Test, Description("Check if the file can be opened")]
        [Category("UnitTest")]
        public void CanOpenFile()
        {
            // Assert: Verify the file stream is not null
            Assert.That(_stream, Is.Not.Null, "File stream should not be null");
        }

        /// <summary>
        /// Read event data from Excel file
        /// </summary>
        [Test, Description("Read event data from Excel file")]
        [Category("UnitTest")]
        public void CanReadEventDataFromExcel()
        {
            // Assert: Verify the event data list is not null
            Assert.That(_eventDataList, Is.Not.Null, "Event Data List should not be null");
            Assert.That(_eventDataList.Count, Is.GreaterThan(0), "Event data list should contain data.");
        }
    }
}
