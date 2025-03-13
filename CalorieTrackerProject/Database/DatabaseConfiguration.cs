using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CalorieTracker
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Method to open and return a new SQL connection
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        // Executes a query and returns a SqlDataReader
        public SqlDataReader ExecuteQuery(string query, Action<SqlCommand> parameterize = null)
        {
            SqlConnection connection = OpenConnection(); // Open connection
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                parameterize?.Invoke(command);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        // Executes a non-query SQL command (like INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, Action<SqlCommand> parameterize = null)
        {
            using (SqlConnection connection = OpenConnection()) // Use 'using' for automatic disposal
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                parameterize?.Invoke(command);
                return command.ExecuteNonQuery();
            }
        }
    }
}