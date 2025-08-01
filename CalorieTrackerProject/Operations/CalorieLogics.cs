using CalorieTracker;
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

    }

    }

