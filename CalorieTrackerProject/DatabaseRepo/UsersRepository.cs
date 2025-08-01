using CalorieTracker;
using Microsoft.Data.SqlClient;

namespace CalorieTrackerProject.DatabaseRepo
{
    public class UserCredential
    {

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

        public static bool Login(string username, string password, DatabaseHelper dbManager)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            var command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username = @Username AND Password = @Password", connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            var result = (int)command.ExecuteScalar();
            if (result > 0)             
            {
                return true;
            }
            else
            {
                return false;
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
    }
}