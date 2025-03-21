using CalorieTracker;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.Operations
{
    internal class CalorieOperator
    {



        public void AddCalorieTracker(CalorieTracker calorieTracker)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO CalorieTracker(UserID, CalorieIntakeTotal, CalorieOutputTotal, BMR, Date) VALUES(@UserID, @CalorieIntakeTotal, @CalorieOutputTotal, @BMR, @Date)", connection);
            command.Parameters.AddWithValue("@UserID", calorieTracker.UserID);
            command.Parameters.AddWithValue("@CalorieIntakeTotal", calorieTracker.CalorieIntakeTotal);
            command.Parameters.AddWithValue("@CalorieOutputTotal", calorieTracker.CalorieOutputTotal);
            command.Parameters.AddWithValue("@BMR", calorieTracker.BMR);
            command.Parameters.AddWithValue("@Date", calorieTracker.Date);
            command.ExecuteNonQuery();
        }


        public void AddCaloriePlan(CaloriePlanModel planModel)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO CaloriePlan (UserID, GoalWeight, DailyCalorieGoal, DateStarted, GoalDate) VALUES (@UserID, @GoalWeight, @DailyCalorieGoal, @DateStarted, @GoalDate)", connection);
            command.Parameters.AddWithValue("@UserID", planModel.UserID);
            command.Parameters.AddWithValue("@GoalWeight", planModel.GoalWeight);
            command.Parameters.AddWithValue("@DailyCalorieGoal", planModel.DailyCalorieGoal);
            command.Parameters.AddWithValue("@DateStarted", planModel.DateStarted);
            command.Parameters.AddWithValue("@GoalDate", planModel.GoalDate);
            command.ExecuteNonQuery();
        }

        internal static void AddCalorieEntry(int userId)
        {
            // Implementation for adding a calorie entry
        }


        internal static void ViewCaloriePlan(int userId)
        {
            // Implementation for viewing a calorie plan
        }

        internal static void ViewDailyCalorieSummary(int userId)
        {
            // Implementation for viewing daily calorie summary
        }

        internal static void ViewMonthlyCalorieSummary(int userId)
        {
            // Implementation for viewing monthly calorie summary
        }

        internal static void CalculateCalorieBassallBurned()
        {
            // implementation for metabolic basall burned
        }

    }
}
