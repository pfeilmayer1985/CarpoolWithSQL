using System.IO;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class UsersDataServiceSQLTests
    {
        UsersDataServiceSQL _usersDataServiceSQL = new UsersDataServiceSQL();

        [TestMethod]
        public void ListAllUsersService_ShouldHaveTheTestUser()
        {
            // Arrange



            // Act


            // Assert
            var testArray = new string[] { "DID#SENVIV,4,Second,Victim,Old Rusty Bicycle,string,string" };
            Assert.AreEqual(testArray[0], result[4]);

        }

        [TestMethod]
        public void AddUserDaService_ShouldAddTheTestUser()
        {

            // Arrange

            UserBaseModelData testUser = new UserBaseModelData()
            {
                ID = 55,
                Email = "testuser@testserver.com",
                Password = "123456",
                PhoneNo = "015302153456",
                FirstName = "MyTest",
                LastName = "UserName",
                IsDriver = true,
            };

            // Act
            _usersDataServiceSQL.AddUserDataService(testUser);
            string resultString = _usersDataServiceSQL.ListUserByIdDataService(testUser.ID);
            var result = resultString.Split(',');
            // Assert
            Assert.AreEqual(testDriver.ID, result[0]);

        }

    }
}