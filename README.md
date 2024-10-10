This project uses the wavacalc15.xls file from Howard Grubb and saves it to MariaDB in a relational format.  
I'm intending to use it in my next home project.
</br></br>
To use it you will need to create an appsettings.json in the ExtractRunningData Project.  
appsettings.json  
```
{
  "DbServer": "localhost",
  "DbPort": "3306",
  "DbName": "Events",
  "DbUser": [UserName],
  "DbPassword": [Password],
  "ExcelFilePath": "D:\\projects\\ExtractRunningData\\wavacalc15.xls"
}
```
Please update and change this file as needed.
