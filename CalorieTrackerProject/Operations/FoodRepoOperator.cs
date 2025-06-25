using CalorieTracker;
using CalorieTrackerProject.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CalorieTrackerProject.Operations
{
    internal class FoodRepoOperator
    {

        internal static void AddFoodList(string name, double caloriePerUnit, string unit)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO FoodList (Name, CaloriePerUnit, Unit) VALUES (@Name, @CaloriePerUnit, @Unit)",
                    connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@CaloriePerUnit", caloriePerUnit);
                command.Parameters.AddWithValue("@Unit", unit);
                command.ExecuteNonQuery();
            }
        }

        internal static void ViewFoodList()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT FoodListId, Name FROM FoodList", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["FoodListId"]}, Name: {reader["Name"]}");
                    }
                }
            }
        }
        internal static Food GetFoodById(int foodId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT FoodListId, Name, CaloriePerUnit, Unit FROM FoodList WHERE FoodListId = @FoodListId",
                    connection);
                command.Parameters.AddWithValue("@FoodListId", foodId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Food
                        {
                            FoodId = Convert.ToInt32(reader["FoodListId"]),
                            Name = reader["Name"].ToString(),
                            Calorieperunit = Convert.ToDouble(reader["CaloriePerUnit"])
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

      

    }
}
