using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class CarpoolDataServiceTests
    {
        CarpoolDataService _carpoolDataService = new CarpoolDataService();

        [TestMethod]
        public void CheckCarpoolInFile()
        {
            // Arrange
            _carpoolDataService.Path = "C:\\010 Projects\\006 Fahrgemeinschaft\\Fahrgemeinschaft\\testcarpools.txt";

            // Act
            string[] result = _carpoolDataService.ListAllCarpoolsDataService();

            // Assert
            var testArray = new string[] { "DID#HANGRO,PID#BENFUR,PID#NICSAN" };
            Assert.AreEqual(testArray[0], result[1]);

        }
    }
}