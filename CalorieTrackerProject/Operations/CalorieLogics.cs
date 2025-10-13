using CalorieTracker;
using CalorieTrackerProject.DatabaseRepo;
using CalorieTrackerProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.Operations
{
    internal class CalorieLogics
    {
        internal static double AddCalorieByFood(Food food)
        {
            var calorie = food.Calorieperunit * food.Amount;
            return calorie;

        }

        internal static double CalculateCalorieBassallBurned(User user)
        {
            int age = DateTime.Now.Year - user.DateOfBirth.Year;

            if (user.Gender == "Male")
            {
                return 88.362 + (13.397 * user.Weight) + (4.799 * user.Height) - (5.677 * age);
            }
            else if (user.Gender == "Female")
            {
                return 447.593 + (9.247 * user.Weight) + (3.098 * user.Height) - (4.330 * age);
            }
            else
            {
                throw new ArgumentException("Gender not recognized. Please specify 'Male' or 'Female'.");
            }
        }

        public static void CreateCaloriePlan(User user)
        {
            Console.WriteLine("Enter your daily calorie goal:");
            if (!double.TryParse(Console.ReadLine(), out double dailyCalorieGoal) || dailyCalorieGoal <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                return;
            }

            Console.WriteLine("Enter your diet type (e.g., Keto, Vegan, etc.):");
            string dietType = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(dietType))
            {
                Console.WriteLine("Invalid input. Please enter a valid diet type:");
                dietType = Console.ReadLine();
            }

            string selectQuery = "SELECT COUNT(*) FROM CaloriePlan WHERE Username = @Username";
            int count = 0;
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = selectQuery;
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@Username";
                    param.Value = user.username;
                    cmd.Parameters.Add(param);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            if (count > 0)
            {
                // Update existing plan
                string updateQuery = "UPDATE CaloriePlan SET DailyCalorieGoal = @Goal, DietType = @Diet WHERE Username = @Username";
                DatabaseHelper.ExecuteNonQuery(updateQuery, cmd =>
                {
                    var goalParam = cmd.CreateParameter();
                    goalParam.ParameterName = "@Goal";
                    goalParam.Value = dailyCalorieGoal;
                    cmd.Parameters.Add(goalParam);

                    var dietParam = cmd.CreateParameter();
                    dietParam.ParameterName = "@Diet";
                    dietParam.Value = dietType;
                    cmd.Parameters.Add(dietParam);

                    var userParam = cmd.CreateParameter();
                    userParam.ParameterName = "@Username";
                    userParam.Value = user.username;
                    cmd.Parameters.Add(userParam);
                });
                Console.WriteLine("Calorie plan updated successfully.");
            }
            else
            {
                CalorieOperator.AddCaloriePlan(new CaloriePlanModel
                {
                    User = user,
                    DailyCalorieGoal = dailyCalorieGoal,
                    DateStarted = DateTime.Now,
                    GoalDate = DateTime.Now.AddMonths(1)
                });
                // Insert new plan
                string insertQuery = "INSERT INTO CaloriePlan (Username, DailyCalorieGoal, DietType) VALUES (@Username, @Goal, @Diet)";
                DatabaseHelper.ExecuteNonQuery(insertQuery, cmd =>
                {
                    var userParam = cmd.CreateParameter();
                    userParam.ParameterName = "@Username";
                    userParam.Value = user.username;
                    cmd.Parameters.Add(userParam);

                    var goalParam = cmd.CreateParameter();
                    goalParam.ParameterName = "@Goal";
                    goalParam.Value = dailyCalorieGoal;
                    cmd.Parameters.Add(goalParam);

                    var dietParam = cmd.CreateParameter();
                    dietParam.ParameterName = "@Diet";
                    dietParam.Value = dietType;
                    cmd.Parameters.Add(dietParam);
                });
                Console.WriteLine("Calorie plan created successfully.");
            }
        }

    }

    }

