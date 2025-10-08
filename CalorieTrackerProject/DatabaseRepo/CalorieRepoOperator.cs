using CalorieTracker;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.DatabaseRepo
{
    internal class CalorieOperator
    {



        public void AddCalorieTracker(CalorieTrackerModel calorieTracker)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO CalorieTracker(UserID, CalorieIntakeTotal, CalorieOutputTotal, BMR, Date) VALUES(@UserID, @CalorieIntakeTotal, @CalorieOutputTotal, @BMR, @Date)", connection);
            //command.Parameters.AddWithValue("@UserID", calorieTracker.User.UserID);
            command.Parameters.AddWithValue("@CalorieIntakeTotal", calorieTracker.CalorieIntakeTotal);
            command.Parameters.AddWithValue("@CalorieOutputTotal", calorieTracker.CalorieOutputTotal);
            command.Parameters.AddWithValue("@BMR", calorieTracker.BMR);
            command.Parameters.AddWithValue("@Date", calorieTracker.Date);
            command.ExecuteNonQuery();
        }


        public bool AddCaloriePlan(CaloriePlanModel planModel)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO CaloriePlan (UserID, GoalWeight, DailyCalorieGoal, DateStarted, GoalDate) VALUES (@UserID, @GoalWeight, @DailyCalorieGoal, @DateStarted, @GoalDate)", connection);
            //command.Parameters.AddWithValue("@UserID", planModel.User.UserID);
            command.Parameters.AddWithValue("@GoalWeight", planModel.GoalWeight);
            command.Parameters.AddWithValue("@DailyCalorieGoal", planModel.DailyCalorieGoal);
            command.Parameters.AddWithValue("@DateStarted", planModel.DateStarted);
            command.Parameters.AddWithValue("@GoalDate", planModel.GoalDate);
            command.ExecuteNonQuery();
            return true;

        }

        internal static void AddCalorieEntry(User user, FoodIntake food)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO CalorieTracker (UserID, Calories, DateTime) VALUES (@UserID, @Calories, @DateTime)", connection);
        }


        internal static void ViewCaloriePlan(User user)
        {
            // Implementation for viewing a calorie plan
        }

        internal static void GetDailyCalorieSummary(User user, DateTime date)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand(
                "SELECT CalorieIntakeTotal, CalorieOutputTotal FROM CalorieTracker WHERE Username = @Username AND CAST(Date AS DATE) = @Date",
                connection);
             command.Parameters.AddWithValue("@Username", user.username);
            command.Parameters.AddWithValue("@Date", date.Date);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                double intake = reader.GetDouble(reader.GetOrdinal("CalorieIntakeTotal"));
                double output = reader.GetDouble(reader.GetOrdinal("CalorieOutputTotal"));
                Console.WriteLine($"Date: {date:yyyy-MM-dd} | Intake: {intake} | Output: {output}");
            }
            else
            {
                Console.WriteLine($"No calorie data found for {date:yyyy-MM-dd}.");
            }
        }

        internal static void GetMonthlyCalorieSummary(User user, int month, int year)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand(
                @"SELECT 
                    SUM(CalorieIntakeTotal) AS TotalIntake, 
                    SUM(CalorieOutputTotal) AS TotalOutput 
                  FROM CalorieTracker 
                  WHERE Username = @Username 
                    AND MONTH(Date) = @Month 
                    AND YEAR(Date) = @Year",
                connection);

             command.Parameters.AddWithValue("@Username", user.username); 
            command.Parameters.AddWithValue("@Month", month);
            command.Parameters.AddWithValue("@Year", year);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                double totalIntake = reader.IsDBNull(reader.GetOrdinal("TotalIntake")) ? 0 : reader.GetDouble(reader.GetOrdinal("TotalIntake"));
                double totalOutput = reader.IsDBNull(reader.GetOrdinal("TotalOutput")) ? 0 : reader.GetDouble(reader.GetOrdinal("TotalOutput"));
                Console.WriteLine($"Month: {month}/{year} | Total Intake: {totalIntake} | Total Output: {totalOutput}");
            }
            else
            {
                Console.WriteLine($"No calorie data found for {month}/{year}.");
            }
        }

    }
}
