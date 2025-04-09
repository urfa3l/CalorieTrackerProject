using CalorieTrackerProject.Database;
using System;
using Microsoft.Data.SqlClient;

namespace CalorieTracker
{
    public class UserCredential
    {
        private DatabaseHelper dbManager;

        public UserCredential(DatabaseHelper dbManager, User userModel)
        {
            this.dbManager = dbManager;
            //this.userModel = userModel;
        }

        public static bool Register(User user)
        {
            var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("INSERT INTO Users (Username, Password, FirstName, LastName, DateOfBirth, Gender, Height, Weight) " +
                                            "VALUES (@Username, @Password, @FirstName, @LastName, @DateOfBirth, @Gender, @Height, @Weight)", connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", user.Gender);
                command.Parameters.AddWithValue("@Height", user.Height);
                command.Parameters.AddWithValue("@Weight", user.Weight);
             
        return true;
        }

        public static int GetUserId(string username, DatabaseHelper dbManager)
        {
            string query = "SELECT UserID FROM Users WHERE Username = @Username";
            using var reader = dbManager.ExecuteQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Username", username);
            });

            if (reader.Read())
            {
                return (int)reader["UserID"];
            }
            return -1;
        //???????
        //making extractor of every attribute
        }

        public static bool Login(string username, string password, DatabaseHelper dbManager)
        {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();
        var command = new SqlCommand("SELECT PasswordHash FROM Users WHERE Username = @Username", connection);
        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Password", password);
            });

            if (reader.Read())
            {
                return (int)reader[0] > 0;
            }
            return false;


        }
    }
}