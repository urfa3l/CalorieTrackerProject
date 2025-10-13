using CalorieTrackerProject;
using CalorieTrackerProject.DatabaseRepo;
using CalorieTrackerProject.Operations;
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
                Console.WriteLine("2. Add / update Diet / Calorie Plan");
                Console.WriteLine("3. View Calorie Plan");
                Console.WriteLine("4. View Daily Calorie Summary");
                Console.WriteLine("5. View Monthly Calorie Summary");
                Console.WriteLine("6. Add Exercise");
                Console.WriteLine("7. Add Exercise List");
                Console.WriteLine("8. Update weight");
                Console.WriteLine("9. Logout");

                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int userMenuChoice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                if (userMenuChoice == 1)
                {
                    FoodIntake food = FoodUI.foodIntakeUI();

                    Console.WriteLine("Do you want to use the current date and time? (y/n):");
                    string useCurrent = Console.ReadLine();
                    DateTime foodDateTime;
                    if (useCurrent?.Trim().ToLower() == "y")
                    {
                        foodDateTime = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("Enter the date of food intake (format: yyyy-MM-dd):");
                        string dateInput = Console.ReadLine();
                        DateTime datePart;
                        while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out datePart))
                        {
                            Console.WriteLine("Invalid date format. Please enter again (format: yyyy-MM-dd):");
                            dateInput = Console.ReadLine();
                        }

                        Console.WriteLine("Enter the time of food intake (format: HH:mm):");
                        string timeInput = Console.ReadLine();
                        TimeSpan timePart;
                        while (!TimeSpan.TryParseExact(timeInput, "hh\\:mm", null, out timePart))
                        {
                            Console.WriteLine("Invalid time format. Please enter again (format: HH:mm):");
                            timeInput = Console.ReadLine();
                        }

                        foodDateTime = datePart.Date + timePart;
                    }

                    CalorieOperator.AddCalorieEntry(user, food, foodDateTime);
                }
                else if (userMenuChoice == 2)
                {
                    CalorieLogics.CreateCaloriePlan(user);
                }
                else if (userMenuChoice == 3)
                {
                    CalorieOperator.ViewCaloriePlan(user);
                }
                else if (userMenuChoice == 4)
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
                else if (userMenuChoice == 5)
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
                else if (userMenuChoice == 6)
                {
                    Console.WriteLine("Enter the exercise name:");
                    string exerciseName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(exerciseName))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid exercise name:");
                        exerciseName = Console.ReadLine();
                    }

                    Console.WriteLine("Enter the duration in minutes:");
                    int duration;
                    while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive integer for duration:");
                    }

                    Console.WriteLine("Do you want to use the current date and time for exercise? (y/n):");
                    string useCurrent = Console.ReadLine();
                    DateTime exerciseDateTime;
                    if (useCurrent?.Trim().ToLower() == "y")
                    {
                        exerciseDateTime = DateTime.Now;
                    }
                    else
                    {
                        Console.WriteLine("Enter the date of exercise (format: yyyy-MM-dd):");
                        string dateInput = Console.ReadLine();
                        DateTime datePart;
                        while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out datePart))
                        {
                            Console.WriteLine("Invalid date format. Please enter again (format: yyyy-MM-dd):");
                            dateInput = Console.ReadLine();
                        }

                        Console.WriteLine("Enter the time of exercise (format: HH:mm):");
                        string timeInput = Console.ReadLine();
                        TimeSpan timePart;
                        while (!TimeSpan.TryParseExact(timeInput, "hh\\:mm", null, out timePart))
                        {
                            Console.WriteLine("Invalid time format. Please enter again (format: HH:mm):");
                            timeInput = Console.ReadLine();
                        }

                        exerciseDateTime = datePart.Date + timePart;
                    }

                    Console.WriteLine("Enter the speed (put 0 if there's no speed):");

                    int speedValue;
                    while (!int.TryParse(Console.ReadLine(), out speedValue) || speedValue < 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a non-negative number for speed (decimals allowed):");
                    }
                    int? speed = speedValue == 0 ? (int?)null : speedValue;

                    Console.WriteLine("Is this exercise static? (true/false):");
                    bool isStatic;
                    while (!bool.TryParse(Console.ReadLine(), out isStatic))
                    {
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false':");
                    }

                    ExerciseOperator.AddExerciseDone(user.username, exerciseName, duration, exerciseDateTime, speed, isStatic);
                }
                else if (userMenuChoice == 7)
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
                else if (userMenuChoice == 8)
                {
                    Console.WriteLine("Enter your new weight (in kg):");
                    double newWeight;
                    while (!double.TryParse(Console.ReadLine(), out newWeight) || newWeight <= 0)
                    {
                        Console.WriteLine("Invalid input, please enter a positive number (decimals allowed):");
                    }
                    UserCredential.UpdateUserWeight(user.username, newWeight);
                }
                else if (userMenuChoice == 9)
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