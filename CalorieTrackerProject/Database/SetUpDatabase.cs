using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
                setTables();
                setExcerciseTypes();
                setFoods();
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
                command = new SqlCommand("create table FoodIntake(\r\nFoodIntakeId int IDENTITY(1,1) PRMIARY KEY, \r\nTrackerId int,\r\nFoodId int,\r\nAmount double,\r\nCalorie_Increase double", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table Exercise(\r\nExerciseId int IDENTITY(1,1) PRIMARY KEY,\r\nTrackerId int,\r\nExercise_TypeId varchar(50),\r\nStarted datetime,\r\nEnded datetime,\r\nSpeed double,\r\nCalories_Burned double,\r\n);", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("create table ExerciseType(\r\nExerciseTypeId int IDENTITY(1,1) PRIMARY KEY,\r\nName varchar(50),\r\nCalorie_Scale double\r\n);", connection);
                command.ExecuteNonQuery();
            }
        }

        public static void setExcerciseTypes()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("insert into ExerciseType(Name, Calorie_Scale) values('Running', 0.1)", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into ExerciseType(Name, Calorie_Scale) values('Walking', 0.05)", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into ExerciseType(Name, Calorie_Scale) values('Cycling', 0.08)", connection);
                command.ExecuteNonQuery();
            }
        }

        public static void setFoods()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("insert into Foods(Food_Name, Calorie_per_Amount, Amount_Type) values('Apple', 52, '100g')", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into Foods(Food_Name, Calorie_per_Amount, Amount_Type) values('Banana', 89, '100g')", connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("insert into Foods(Food_Name, Calorie_per_Amount, Amount_Type) values('Chicken Breast', 165, '100g')", connection);
                command.ExecuteNonQuery();
            }
        }

        internal static void SetupDatabase()
        {
            throw new NotImplementedException();
        }
    }
}