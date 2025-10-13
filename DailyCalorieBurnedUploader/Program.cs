using System;
using CalorieTracker;
using CalorieTrackerProject.DatabaseRepo;
using CalorieTrackerProject.Entities; // Adjust namespace if needed

namespace DailyCalorieBurnedUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = UserCredential.GetAllUsers(); 
            foreach (var user in users)
            {
                double burnedCalories = CalculateDailyBurnedCalories(user);
                UploadDailyBurnedCalories(user, DateTime.Today, burnedCalories);
            }
        }

        private static double CalculateDailyBurnedCalories(User user)
        {
            int age = DateTime.Today.Year - user.DateOfBirth.Year;
            if (user.DateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

            if (user.Gender?.ToLower() == "male" || user.Gender?.ToLower() == "m")
                return 10 * user.Weight + 6.25 * user.Height - 5 * age + 5;
            else if (user.Gender?.ToLower() == "female" || user.Gender?.ToLower() == "f")
                return 10 * user.Weight + 6.25 * user.Height - 5 * age - 161;
            else
                return 10 * user.Weight + 6.25 * user.Height - 5 * age;
        }

        private static void UploadDailyBurnedCalories(User user, DateTime date, double burnedCalories)
        {
            string query = "INSERT INTO DailyCalorieBurned (Username, Date, BurnedCalories) VALUES (@username, @date, @burnedCalories)";
            DatabaseHelper.ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Parameters.AddWithValue("@burnedCalories", burnedCalories);
            });
        }
    }
}