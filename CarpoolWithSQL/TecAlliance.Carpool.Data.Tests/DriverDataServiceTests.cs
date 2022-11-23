using System.IO;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class DriverDataServiceTests
    {
        DriverDataService _driverDataService = new DriverDataService();

        [TestMethod]
        public void ListAllDriversService_ShouldHaveTheTestDriverInTheFile()
        {
            // Arrange

            _driverDataService.Path = "C:\\010 Projects\\006 Fahrgemeinschaft\\Fahrgemeinschaft\\testdrivers.txt";

            // Act
            string[] result = _driverDataService.ListAllDriversService();

            // Assert
            var testArray = new string[] { "DID#SENVIV,4,Second,Victim,Old Rusty Bicycle,string,string" };
            Assert.AreEqual(testArray[0], result[4]);

        }

        [TestMethod]
        public void AddDriverDaService_ShouldAddTheTestDriverToFile()
        {
            
            // Arrange
            _driverDataService.Path = "C:\\010 Projects\\006 Fahrgemeinschaft\\Fahrgemeinschaft\\testdrivers.txt";
            DriverModelData testDriver = new DriverModelData()
            {
                ID = "DID#JOHDOE",
                FreePlaces = 5,
                FirstName = "John",
                LastName = "Doe",
                CarTypeMake = "Some Car",
                StartingCity = "Kansas",
                Destination = "Narnia"
            };

            // Act
            _driverDataService.AddDriverDaService(testDriver);
            string resultString = _driverDataService.ListAllDriversService().First(result => result.Contains(testDriver.ID));
            var result = resultString.Split(',');
            // Assert
            Assert.AreEqual(testDriver.ID, result[0]);

        }

    }
}