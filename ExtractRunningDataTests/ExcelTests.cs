﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRunningDataTests
{
    internal class ExcelTests : TestSetupBase
    {
        [Test, Description("Check if the file can be opened")]
        public void CanOpenFile()
        {
            // Assert: Verify the file stream is not null
            Assert.That(_stream, Is.Not.Null);
        }

        [Test, Description("Read event data from Excel file")]
        public void CanReadEventDataFromExcel()
        {
            // Assert: Verify the event data list is not null
            Assert.That(_eventDataList, Is.Not.Null);
        }
    }
}
