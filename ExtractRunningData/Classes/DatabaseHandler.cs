﻿using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace ExtractRunningData.Classes
{
    public static class DatabaseHandler
    {
        public static bool DoesTableExist(DbContext context, string tableName)
        {
            return context.Database.GetDbConnection().GetSchema("Tables")
                .Rows.Cast<System.Data.DataRow>()
                .Any(row => row["TABLE_NAME"].ToString() == tableName);
        }

        public static void SaveEventDataToDatabase(EventDataContext context, List<RunningEventData> EventDataList)
        {
            try
            {
                // Our test database is in-memory, so we need to check if the table exists
                if (context.Database.IsRelational())
                {
                    context.Database.EnsureCreated();
                    context.Database.ExecuteSql($"DELETE FROM runningeventdata");
                    context.Database.ExecuteSql($"DELETE FROM runningeventfactors");
                }
                context.RunningEventData.AddRange(EventDataList);
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error saving data: {ex.InnerException?.Message}");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : {ex.Message}");
            }
        }
    }
}