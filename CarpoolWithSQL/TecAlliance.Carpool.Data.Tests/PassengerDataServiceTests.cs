

using Moq;
using System.Reflection;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class PassengerDataServiceTests
    {
        PassengerDataService _passengerDataService = new PassengerDataService();
      
        [TestMethod]
        public void CheckPassengerInFileTest()
        {
            // Arrange
            _passengerDataService.Path = "C:\\010 Projects\\006 Fahrgemeinschaft\\Fahrgemeinschaft\\testpassengers.txt";


            // Act
            string[] result = _passengerDataService.ListAllPassengersService();

            // Assert
            var testArray = new string[] { "PID#NICSAN,Nicusor,Sandu,Start,End" };
            Assert.AreEqual(testArray[0], result[0]);

        }

        [TestMethod]
        public void AddNewPassengerToFileTest()
        {
            // Arrange
            _passengerDataService.Path = "C:\\010 Projects\\006 Fahrgemeinschaft\\Fahrgemeinschaft\\testpassengers.txt";
            PassengerModelData testPassenger = new PassengerModelData()
            {
                ID = "PID#JOHDOE",
                FirstName = "John",
                LastName = "Doe",
                StartingCity = "Kansas",
                Destination = "Narnia"
            };

            // Act
            _passengerDataService.AddPassengerDaService(testPassenger);
            string resultString = _passengerDataService.ListAllPassengersService().First(result => result.Contains(testPassenger.ID));
            var result = resultString.Split(',');
            // Assert
            Assert.AreEqual(testPassenger.ID, result[0]);
        }

       
    }
}