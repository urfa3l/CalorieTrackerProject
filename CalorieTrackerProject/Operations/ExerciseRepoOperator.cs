using CalorieTracker;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.Operations
{
    internal class ExerciseOperator
    {
        internal static void AddExerciseList(string name, double caloriePerMinute, bool isstatic)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO ExcerciseList (Name, CaloriePerMinute, IsStatic) VALUES (@Name, @CaloriePerMinute, @IsStatic)",
                    connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@CaloriePerMinute", caloriePerMinute);
                command.Parameters.AddWithValue("@IsStatic", isstatic);
                command.ExecuteNonQuery();
            }
        }
        internal static void RemoveExerciseList(string name)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM ExcerciseList WHERE Name = @Name", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.ExecuteNonQuery();
            }
        }
        internal static void AddExercise(int userId, string name, int durationinminute, DateTime date, int? speed, bool isstatic)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Excercise (UserId, Excercise, DurationInMinute, Date, Speed, IsStatic) VALUES (@UserId, @Name, @Duration, @Date, @Speed, @IsStatic)",
                    connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Duration", durationinminute);
                command.Parameters.AddWithValue("@Date", date);
                command.Parameters.AddWithValue("@Speed", (object)speed ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsStatic", isstatic);
                command.ExecuteNonQuery();
            }
        }


    }
}
