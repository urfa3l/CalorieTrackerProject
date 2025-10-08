using CalorieTrackerProject;
using CalorieTrackerProject.DatabaseRepo;
using CalorieTrackerProject.UI;
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
            bool loggedin = false;
            User user = new User();
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
                    user = UserUI.CreateUser();
                    Console.WriteLine("Enter your username: ");
                    var username = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(username))
                    {
                        Console.WriteLine("Invalid input. please retry");
                        username = Console.ReadLine();
                    }
                    Console.WriteLine("Enter your password: ");
                    var password = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Invalid input. please retry");
                        password = Console.ReadLine();
                    }
                    loggedin = UserCredential.Register(user, username, password);
                    UserMenu(user);
                }
                else if (menuChoice == 2)
                {
                    Console.WriteLine("Enter your username: ");
                    var username = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(username))
                    {
                        Console.WriteLine("Invalid input. please retry");
                        username = Console.ReadLine();
                    }
                    Console.WriteLine("Enter your password: ");
                    var password = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Invalid input. please retry");
                        password = Console.ReadLine();
                    }
                    user = UserCredential.Login(username, password);
                    UserMenu(user);
                }
                else if (menuChoice == 3)
                {
                    SetUpDatabase.setDatabase();
                }
                else if (menuChoice == 4)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        private static void UserMenu(User user)
        {
            bool logout = false;
            DateTime dateTime = DateTime.Now;
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
                    FoodIntake food = FoodUI.foodIntakeUI();
                    CalorieOperator.AddCalorieEntry(user, food);
                }
                else if (userMenuChoice == 2)
                {
                    //CalorieOperator.ViewCaloriePlan(userId);
                }
                else if (userMenuChoice == 3)
                {

                    Console.WriteLine("Put in which month you want to see:");
                    var monthInput = Console.ReadLine();

                    Console.WriteLine("Put in which day you want to see:");
                    var dayInput = Console.ReadLine();

                    Console.WriteLine("Put in which year you want to see:");
                    var yearInput = Console.ReadLine();

                    if (int.TryParse(yearInput, out int year) && int.TryParse(monthInput, out int month) && int.TryParse(dayInput, out int day))
                    {
                        if (year > dateTime.Year)
                        {
                            Console.WriteLine("The year is in the future.");
                        }
                        else
                        {
                            Console.WriteLine("The year is not in the future.");
                        }

                        if (month < 1 || month > 12)
                        {
                            Console.WriteLine("Invalid month. Please enter a value between 1 and 12.");
                        }
                        else if (day < 1 || day > DateTime.DaysInMonth(year, month))
                        {
                            Console.WriteLine($"Invalid day. Please enter a value between 1 and {DateTime.DaysInMonth(year, month)} for the given month and year.");
                        }
                        else
                        {
                            DateTime inputDate = new DateTime(year, month, day);
                            CalorieOperator.GetDailyCalorieSummary(user, inputDate);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please make sure to enter numeric values for day, month, and year.");
                    }
                }
                else if (userMenuChoice == 4)
                {
                    DateTime now = DateTime.Now;

                    Console.WriteLine("Put in which month you want to see:");
                    var monthInput = Console.ReadLine();

                    Console.WriteLine("Put in which year you want to see:");
                    var yearInput = Console.ReadLine();

                    if (int.TryParse(monthInput, out int month) && int.TryParse(yearInput, out int year))
                    {
                        if (month < 1 || month > 12)
                        {
                            Console.WriteLine("Invalid input. Please retry.");
                            return; 
                        }

                        if (year < 1 || year > now.Year)
                        {
                            Console.WriteLine("Invalid input. Please retry.");
                            return;
                        }

                        CalorieOperator.GetMonthlyCalorieSummary(user, month, year);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please make sure to enter numeric values for month and year.");
                    }
                }
                else if (userMenuChoice == 5)
                {
                    ExerciseOperator.AddExercise(userId);
                }
                else if (userMenuChoice == 6)
                {
                    Console.WriteLine("Input the name: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Input the calorie burned per minute: ");
                    double caloriePerMinute;
                    while (!double.TryParse(Console.ReadLine(), out caloriePerMinute) || caloriePerMinute <= 0)
                    {
                        Console.WriteLine("Invalid input, please enter a positive number (decimals allowed):");

                    }
                    Console.WriteLine("Is this exercise static? (true/false): ");
                    bool isStatic;
                    while (!bool.TryParse(Console.ReadLine(), out isStatic))
                    {
                        Console.WriteLine("Invalid input, please enter 'true' or 'false':");
                    }
                    ExerciseOperator.AddExerciseList(name, caloriePerMinute, isStatic);

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