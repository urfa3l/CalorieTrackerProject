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
                var command = new SqlCommand("create table User(\r\nUserId INT IDENTITY(1,1) PRIMARY KEY,\r\nUsername varchar(50),\r\nPassword varchar(50),\r\nFirstName varchar(50),\r\nLastName varchar(50),\r\nDateOfBirth Date,\r\nGender varchar(10),\r\nHeight double,\r\nWeight double\r\n);", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table Calorie_Plan(\r\nPlanId INT IDENTITY(1,1) PRIMARY KEY,\r\nUserId int,\r\nGoal_Weight double,\r\nDailyCalorieGoal double,\r\nDate_Started date,\r\nGoal_Date date\r\n);", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table CalorieTracker(\r\nTrackerId int IDENTITY(1,1) PRIMARY KEY,\r\nUserId int,\r\nCalorie_Intake_Total double,\r\nCalorie_Output_Total double,\r\nBMR double,\r\nDate date\r\n);", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table Foods(\r\nFoodId int IDENTITY(1,1) PRIMARY KEY,\r\nFood_Name varchar(50),\r\nCalorie_per_Amount double,\r\nAmount_Type Varchar(50)\r\n);", connection);
                command.ExecuteNonQuery();


            }
        }
    }
}