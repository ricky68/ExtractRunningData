
using ExtractRunningData;
using ExtractRunningData.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

IConfigurationRoot configuration = builder.Build();

string currentPath = Directory.GetCurrentDirectory();
Console.WriteLine("Current Path: " + currentPath);

var optionsBuilder = new DbContextOptionsBuilder<EventDataContext>();

optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection")
    , new MySqlServerVersion(new Version(11, 5, 2)));
var _context = new EventDataContext(optionsBuilder.Options);

var eventDataList = ExcelFileHandler.ProcessExcelFile(configuration["ExcelFilePath"] ?? @"C:\projects\ExtractRunningData\ExtractRunningData\wavacalc15.xls");
DatabaseHandler.SaveEventDataToDatabase(_context, eventDataList);

