using CalorieTracker;
using CalorieTrackerProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.Operations
{
    internal class CalorieLogics
    {
        internal static AddCalorieByFood(User user)
        {

            Console.Write("Enter your Food name from the list: ");

            FoodRepoOperator.ViewFoodList();

            if (!int.TryParse(Console.ReadLine(), out int menuChoice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
            }

            Console.Write("Enter the amount of food consumed: ");

            if (!double.TryParse(Console.ReadLine(), out double foodAmount))
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }

            Food food = FoodRepoOperator.GetFoodById(menuChoice);

            if (food == null)
            {
                Console.WriteLine("Food not found. Please try again.");
                return;
            }

            else
            {
                double totalCalories = food.Calorieperunit * foodAmount;
            }

        }

    }
}
