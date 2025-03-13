using CalorieTrackerProject.Database;
using System;

namespace CalorieTracker
{
    public class CaloriePlan
    {
        private DatabaseHelper dbManager;
        private CaloriePlanModel planModel;

        public CaloriePlan(DatabaseHelper dbManager, CaloriePlanModel planModel)
        {
            this.dbManager = dbManager;
            this.planModel = planModel;
        }

        public void AddCaloriePlan()
        {
            string query = "INSERT INTO CaloriePlan (UserID, GoalWeight, DailyCalorieGoal, DateStarted, GoalDate) VALUES (@UserID, @GoalWeight, @DailyCalorieGoal, @DateStarted, @GoalDate)";
            dbManager.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@UserID", planModel.UserID);
                command.Parameters.AddWithValue("@GoalWeight", planModel.GoalWeight);
                command.Parameters.AddWithValue("@DailyCalorieGoal", planModel.DailyCalorieGoal);
                command.Parameters.AddWithValue("@DateStarted", planModel.DateStarted);
                command.Parameters.AddWithValue("@GoalDate", planModel.GoalDate);
            });
        }
    }
}