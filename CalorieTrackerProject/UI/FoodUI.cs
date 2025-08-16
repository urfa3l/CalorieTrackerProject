using CalorieTracker;
using CalorieTrackerProject.DatabaseRepo;
using CalorieTrackerProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject.UI
{
    internal class FoodUI
    {
        internal static FoodIntake foodIntakeUI()
        {

            Console.Write("Enter your Food name from the list: ");

            FoodRepository.ViewFoodList();

            string foodName = Console.ReadLine();

            Food food = FoodRepository.GetFoodByName(foodName);
            if (food == null)
            {
                Console.WriteLine("Food not found. Please try again.");
                return null;
            }
            else
            {
                Console.Write("Enter the amount of food consumed: ");

                double foodAmount;
                while (true)
                {
                    if (!double.TryParse(Console.ReadLine(), out foodAmount))
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                        Console.Write("Enter the amount of food consumed: ");
                    }
                    else
                    {
                        break;
                    }
                }
                    food.Amount = foodAmount;
                FoodIntake foodIntake = new FoodIntake();
                foodIntake.FoodID = food.FoodId;
                foodIntake.Amount = foodAmount;
                foodIntake.CalorieIncrease = foodAmount * food.Calorieperunit;

                return foodIntake;

            }
        }
    }
}
