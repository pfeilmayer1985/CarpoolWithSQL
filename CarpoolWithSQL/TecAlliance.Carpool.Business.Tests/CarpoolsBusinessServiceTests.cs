using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Authorization;
using Moq;
using System.Security.Cryptography;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;

namespace TecAlliance.Carpool.Business.Tests
{
    [TestClass]
    public class CarpoolsBusinessServiceTests
    {
        CarpoolsBusinessService _carpoolBusinessService = new CarpoolsBusinessService();

        [TestMethod]
        public void ListAllCarpoolsDataBu_ShouldListAllInDB()
        {
            //Arrange

            //Creating a mock
            var carPoolDataServiceMock = new Mock<ICarpoolsDataServiceSQL>();
            carPoolDataServiceMock.Setup(x => x.ListAllCarpoolsDataService())
            .Returns(GetSampleCarpools);

            var userDataServiceMock = new Mock<IUsersDataServiceSQL>();
            userDataServiceMock.Setup(x => x.ListAllUsersDataService())
            .Returns(GetSampleUsers);

            var carpoolBusinessService = new CarpoolsBusinessService(carPoolDataServiceMock.Object, userDataServiceMock.Object);

            //Act
            var result = carpoolBusinessService.ListAllCarpoolsBusinessService();

            //Assert
            Assert.AreEqual(4, result.Count);

            var expectedItem = GetSampleCarpools()[2];
            var splittedExpectedItem = expectedItem.Split(',');
            bool expectedItemFound = false;
            foreach (var resultItem in result)
            {
                if (resultItem. == splittedExpectedItem[0])
                {
                    expectedItemFound = true;
                }
            }
            Assert.IsTrue(expectedItemFound);

            bool expectedResultFound = false;
            int passengers = 0;
            foreach (var resultItem in result)
            {
                for (int i = 0; i <= resultItem.Passengers.ToList().Count() - 1; i++)
                {
                    if (resultItem.Passengers[i] == splittedExpectedItem[i])
                    {
                        passengers++;
                    }
                }
                if (passengers == resultItem.Passengers.ToList().Count())
                {
                    expectedResultFound = true;
                }
            }
            Assert.IsTrue(expectedResultFound);


        }

        private string[] GetSampleCarpools()
        {
            string[] result = new string[]
            {
                "DID#ANDPFE,PID#RAUCIC,PID#ROBPFE,PID#BENFUR",
                "DID#HANGRO,PID#BENFUR,PID#NICSAN",
                "DID#PETMAX,PID#ROBPFE,PID#VICEGR",
                "DID#SECVIC,PID#NICSAN,PID#ROBPFE"
            };
            return result;
        }

        private string[] GetSampleUsers()
        {
            string[] result = new string[]
            {
                "DID#PETMAX,4,Petre,Maxim,Volvo XC 60,Baia Mare,End",
                "DID#SECVIC,4,Second,Victim,Batmobil,string,string",
               
            };
            return result;
        }

      
    }
}