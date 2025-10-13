using CalorieTracker;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTrackerProject
{
    internal class UserUI
    {
        public static User CreateUser()
        {
            User user = new User();
            Console.Write("Enter your first name: ");
            user.FirstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(user.FirstName))
            {
                Console.WriteLine("Invalid input. First name cannot be empty. Please retry:");
                user.FirstName = Console.ReadLine();
            }
            Console.WriteLine("Enter your last name: ");
            user.LastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(user.LastName))
            {
                Console.WriteLine("Invalid input. Last name cannot be empty. Please retry:");
                user.LastName = Console.ReadLine();
            }
            Console.WriteLine("Enter your date of birth: ");
            var dt = Console.ReadLine();
            DateTime dateofbirth;
            while (!DateTime.TryParseExact(dt, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateofbirth))
            {
                Console.WriteLine("Invalid date, please retry (format: DD/MM/YYYY)");
                dt = Console.ReadLine();
            }
            user.DateOfBirth = dateofbirth;
            Console.WriteLine("Enter your assigned gender at birth (male / female)");
            string gender = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(gender) || !(gender.ToLower() == "male" || gender.ToLower() == "female"))
            {
                Console.WriteLine("Invalid input. Please enter 'male' or 'female':");
                gender = Console.ReadLine();
            }
            user.Gender = gender;

            Console.WriteLine("Enter your weight (in kg)");
            double weight;
            while (!double.TryParse(Console.ReadLine(), out weight) || weight <= 0)
            {
                Console.WriteLine("Invalid input, please enter a positive number (decimals allowed):");

            }
            user.Weight = weight;

            Console.WriteLine("Enter your height (in cm)");
            double height;
            while (!double.TryParse(Console.ReadLine(), out height) || height <= 0 || height > 300)
            {
                Console.WriteLine("Invalid input, please enter a positive number up to 300 (decimals allowed):");
            }
            user.Height = height;

            return user;

        }
    }
}
