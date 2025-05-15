using Microsoft.Data.SqlClient;

namespace CalorieTracker
{
    public class UserCredential
    {
        private DatabaseHelper dbManager;

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
    }
}