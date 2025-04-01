using System;

namespace CalorieTracker
{
    public class CaloriePlanModel
    {
        public UserModel User { get; set; }
        public double GoalWeight { get; set; }
        public double DailyCalorieGoal { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime GoalDate { get; set; }
    }
}