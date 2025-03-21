using CalorieTracker;
using CalorieTrackerProject.Functions.Excercise;
using CalorieTrackerProject.Operations;
using System;

namespace CalorieTracker
{
    public class Program
    {
        private static DatabaseHelper dbManager;

        public static void Main()
        {

            Console.WriteLine("Welcome to the Calorie Tracker!");

            MainMenu();

            Console.WriteLine("Thank you for using the Calorie Tracker!");
        }

        private static void MainMenu()
        {
            bool exit = false;
            int userId = -1;
            while (!exit)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Log in");
                Console.WriteLine("3. Set up databases");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                if (menuChoice == 1)
                {
                    userId = UserCredential.Register();
                }
                else if (menuChoice == 2)
                {
                    userId = UserCredential.Login();
                    UserMenu(userId)
                }
                else if (menuChoice == 3)
                {
                    SetUpDatabase.SetupDatabase();
                }
                else if (menuChoice == 4)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                if (userId != -1)
                {
                    UserMenu(userId);
                }
            }
        }

        private static void UserMenu(int userId)
        {
            bool logout = false;
            while (!logout)
            {
                Console.WriteLine("\nUser Menu:");
                Console.WriteLine("1. Add Food eaten");
                Console.WriteLine("2. View Calorie Plan");
                Console.WriteLine("3. View Daily Calorie Summary");
                Console.WriteLine("4. View Monthly Calorie Summary");
                Console.WriteLine("5. Add Exercise");
                Console.WriteLine("6. Add Exercise List");
                Console.WriteLine("7. Logout");

                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int userMenuChoice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                if (userMenuChoice == 1)
                {
                    CalorieOperator.AddCalorieEntry(userId);
                }
                else if (userMenuChoice == 2)
                {
                    CalorieOperator.ViewCaloriePlan(userId);
                }
                else if (userMenuChoice == 3)
                {
                    CalorieOperator.ViewDailyCalorieSummary(userId);
                }
                else if (userMenuChoice == 4)
                {
                    CalorieOperator.ViewMonthlyCalorieSummary(userId);
                }
                else if (userMenuChoice == 5)
                {
                    AddExercise.AddExercise(userId);
                }
                else if (userMenuChoice == 6)
                {
                    AddExercise.AddExercise(userId);
                }
                else if(userMenuChoice == 7)
                {
                    logout = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }
    }
}