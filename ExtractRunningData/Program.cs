// See https://aka.ms/new-console-template for more information

using ExtractRunningData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

IConfigurationRoot configuration = builder.Build();

var optionsBuilder = new DbContextOptionsBuilder<EventDataContext>();
//optionsBuilder.UseSqlServer(Configuration.GetConnectionStringSecureValue("DefaultConnection"));
optionsBuilder.UseMySql(
    $"server={configuration["DbServer"]};port={configuration["DbPort"]};database={configuration["DbName"]};user={configuration["DbUser"]};password={configuration["DbPassword"]}",
    new MySqlServerVersion(new Version(11, 5, 2))
    );
var _context = new EventDataContext(optionsBuilder.Options);

using var openExcelFile = new OpenExcelFile(_context);
openExcelFile.ProcessExcelFile(filePath: configuration["ExcelFilePath"] ?? @"D:\projects\ExtractRunningData\wavacalc15.xls");
openExcelFile.Dispose();

