using CalorieTracker;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.DatabaseRepo
{
    internal class ExerciseOperator
    {
        internal static void AddExerciseList(string name, double caloriePerMinute, bool isstatic)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO ExcerciseType (Name, CaloriePerMinute, IsStatic) VALUES (@Name, @CaloriePerMinute, @IsStatic)",
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
                var command = new SqlCommand("DELETE FROM ExcerciseType WHERE Name = @Name", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.ExecuteNonQuery();
            }
        }
        internal static void AddExerciseDone(string username, string name, int durationinminute, DateTime date, int? speed, bool isstatic)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO Excercise (Username, Excercise, DurationInMinute, Date, Speed, IsStatic) VALUES (@UserId, @Name, @Duration, @Date, @Speed, @IsStatic)",
                    connection);
                command.Parameters.AddWithValue("@Username", username);
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
