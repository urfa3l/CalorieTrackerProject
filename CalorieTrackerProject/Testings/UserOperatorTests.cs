using CalorieTracker;
using System;
using NUnit.Framework;


namespace CalorieTrackerProject.Testings
{
    [TestFixture]
    public class UserOperatorTests
    {
        private DatabaseHelper _dbHelper;

        [SetUp]
        public void SetUp()
        {
            // Initialize the database helper and set up the database
            _dbHelper = new DatabaseHelper();
            SetUpDatabase.SetupDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the database after each test
            _dbHelper.ExecuteNonQuery("DELETE FROM Users");
        }

        [Test]
        public void Register_ShouldInsertUserIntoDatabase()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Password = "password123",
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                Height = 180,
                Weight = 75
            };

            // Act
            var result = UserCredential.Register(user);

            // Assert
            Assert.IsTrue(result, "Register method should return true on success.");

            var query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            var count = _dbHelper.ExecuteScalar<int>(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
            });

            Assert.AreEqual(1, count, "User should be inserted into the database.");
        }
        [Test]
        public void Login_ShouldReturnTrueForValidCredentials()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";

            // Act
            var result = UserCredential.Login(username, password, _dbHelper);

            // Assert
            Assert.IsTrue(result, "Login should return true for valid credentials.");
        }

        [Test]
        public void Login_ShouldReturnFalseForInvalidCredentials()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";

            // Act
            var result = UserCredential.Login(username, password, _dbHelper);

            // Assert
            Assert.IsFalse(result, "Login should return false for invalid credentials.");
        }

        [Test]
        public void Login_ShouldReturnFalseForNonExistentUser()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "password123";

            // Act
            var result = UserCredential.Login(username, password, _dbHelper);

            // Assert
            Assert.IsFalse(result, "Login should return false for a non-existent user.");
        }
    }
}
