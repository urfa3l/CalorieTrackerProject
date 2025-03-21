using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CalorieTracker
{
    public class DatabaseHelper
    {
        private static string connectionString = "Data Source=(localdb)\\local;Initial Catalog=miniproject;Integrated Security=true;";

        private static string initialconnectionString = "Data Source=(localdb)\\local;Integrated Security=true;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        public static SqlConnection GetBase()
        {
            return new SqlConnection(initialconnectionString);
        }

        // Method to open and return a new SQL connection
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        // Executes a query and returns a SqlDataReader
        public static SqlDataReader ExecuteQuery(string query, Action<SqlCommand> parameterize = null)
        {
            SqlConnection connection = OpenConnection(); // Open connection
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        // Executes a non-query SQL command (like INSERT, UPDATE, DELETE)
        public static int ExecuteNonQuery(string query, Action<SqlCommand> parameterize = null)
        {
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return command.ExecuteNonQuery();
            }
        }
    }
}