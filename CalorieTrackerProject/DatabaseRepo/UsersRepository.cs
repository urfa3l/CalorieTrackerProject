using CalorieTracker;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace CalorieTrackerProject.DatabaseRepo
{
    public class UserCredential
    {

        public static bool Register(User user, string Username, string Password)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO Users (Username, Password, FirstName, LastName, DateOfBirth, Gender, Height, Weight) " +
                                            "VALUES (@Username, @Password, @FirstName, @LastName, @DateOfBirth, @Gender, @Height, @Weight)", connection);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@Height", user.Height);
                command.Parameters.AddWithValue("@Weight", user.Weight);
             
        return true;
        }

        public static User Login(string username, string password)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password", connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            using var reader = command.ExecuteReader();
            if (reader.Read()) 
            {
                var user = new User
                {
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")), 
                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                    Height = reader.GetDouble(reader.GetOrdinal("Height")), 
                    Weight = reader.GetDouble(reader.GetOrdinal("Weight"))  
                };

                return user; 
            }
            else
            {
                return null;
            }


        }

        public static User GetUserByUserName(string username)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("SELECT UserID, Username, Password, FirstName, LastName, DateOfBirth, Gender, Height, Weight FROM Users WHERE Username = @Username", connection);
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                    Height = reader.GetDouble(reader.GetOrdinal("Height")),
                    Weight = reader.GetDouble(reader.GetOrdinal("Weight"))
                };
            }
            return null;
        }

        public static List<User> GetAllUsers()
        {
            var users = new List<User>();
            string query = "SELECT FirstName, LastName, DateOfBirth, Gender, Height, Weight, username FROM Users";
            using (var reader = DatabaseHelper.ExecuteQuery(query))
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Gender = reader["Gender"].ToString(),
                        Height = Convert.ToDouble(reader["Height"]),
                        Weight = Convert.ToDouble(reader["Weight"]),
                        username = reader["username"].ToString()
                    });
                }
            }
            return users;
        }

        public static void UpdateUserWeight(string username, double newWeight)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("UPDATE Users SET Weight = @Weight WHERE Username = @Username", connection);
            command.Parameters.AddWithValue("@Weight", newWeight);
            command.Parameters.AddWithValue("@Username", username);
            command.ExecuteNonQuery();
        }
    }
}