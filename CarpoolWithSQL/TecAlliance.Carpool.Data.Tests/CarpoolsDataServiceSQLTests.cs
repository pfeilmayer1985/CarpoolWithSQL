using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class CarpoolsDataServiceSQLTests
    {
        CarpoolsDataServiceSQL _carpoolsDataServiceSQL = new CarpoolsDataServiceSQL();

        [TestMethod]
        public void CheckCarpoolExist()
        {
            // Arrange
            

            // Act
            string[] result = _carpoolDataService.ListAllCarpoolsDataService();

            // Assert
            var testArray = new string[] { "DID#HANGRO,PID#BENFUR,PID#NICSAN" };
            Assert.AreEqual(testArray[0], result[1]);

        }
    }
}