using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalorieTracker
{
    internal class SetUpDatabase
    {
        public static void setDatabase()
        {
            using (var connection = DatabaseHelper.GetBase())
            {
                connection.Open();
                var command = new SqlCommand("create database CalorieTracker", connection);
                command.ExecuteNonQuery();
            }
        }

        public static void setTables()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("create table User(\r\UserId INT IDENTITY(1,1) PRIMARY KEY,\r\nUsername varchar(50),\r\nPassword varchar(50),\r\nFirst_Name);", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table Transactions(\r\nTransactionId INT IDENTITY(1,1) PRIMARY KEY,\r\nUserId int,\r\nDate datetime,\r\nAmount int,\r\ncategoryId int\r\n);", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table Users(\r\nUserId int IDENTITY(1,1) PRIMARY KEY,\r\nUsername varchar(50),\r\nPasswordHash varchar(100)\r\n);", connection);
                command.ExecuteNonQuery();

            }
        }
}
