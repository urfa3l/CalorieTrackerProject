using CalorieTrackerProject.Database;
using System;
using System.Data.SqlClient;

namespace CalorieTracker
{
    public class UserCredential
    {
        private DatabaseHelper dbManager;
        private UserModel userModel;

        public UserCredential(DatabaseHelper dbManager, UserModel userModel)
        {
            this.dbManager = dbManager;
            this.userModel = userModel;
        }

        public static bool Register()
        {
            string query = "INSERT INTO Users (Username, Password, FirstName, LastName, DateOfBirth, Gender, Height, Weight) " +
                           "VALUES (@Username, @Password, @FirstName, @LastName, @DateOfBirth, @Gender, @Height, @Weight)";
            return dbManager.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Username", userModel.Username);
                command.Parameters.AddWithValue("@Password", userModel.Password);
                command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                command.Parameters.AddWithValue("@LastName", userModel.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", userModel.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", userModel.Gender);
                command.Parameters.AddWithValue("@Height", userModel.Height);
                command.Parameters.AddWithValue("@Weight", userModel.Weight);
            }) > 0;
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
        }

        public static bool Login(string username, string password, DatabaseHelper dbManager)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
            using var reader = dbManager.ExecuteQuery(query, command =>
            {
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