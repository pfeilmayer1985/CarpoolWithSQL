using Autofac.Extras.Moq;
using Microsoft.AspNetCore.Authorization;
using Moq;
using System.Security.Cryptography;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;

namespace TecAlliance.Carpool.Business.Tests
{
    [TestClass]
    public class CarpoolBusinessServiceTests
    {
        CarpoolsBusinessService _carpoolBusinessService = new CarpoolsBusinessService();

        [TestMethod]
        public void ListAllCarpoolsDataBu_ShouldListAllInFile()
        {
            //Arrange

            //Creating a mock
            var carPoolDataServiceMock = new Mock<ICarpoolDataService>();
            carPoolDataServiceMock.Setup(x => x.ListAllCarpoolsDataService())
            .Returns(GetSampleCarpools);

            var driverDataServiceMock = new Mock<IDriverDataService>();
            driverDataServiceMock.Setup(x => x.ListAllDriversService())
            .Returns(GetSampleDrivers);

            var passengerDataServiceMock = new Mock<IPassengerDataService>();
            passengerDataServiceMock.Setup(x => x.ListAllPassengersService())
            .Returns(GetSamplePassengers);

            var carpoolBusinessService = new CarpoolBusinessService(carPoolDataServiceMock.Object, driverDataServiceMock.Object, passengerDataServiceMock.Object);

            //Act
            var result = carpoolBusinessService.ListAllCarpoolsDataBu();

            //Assert
            Assert.AreEqual(4, result.Length);

            var expectedItem = GetSampleCarpools()[2];
            var splittedExpectedItem = expectedItem.Split(',');
            bool expectedItemFound = false;
            foreach (var resultItem in result)
            {
                if (resultItem.Driver == splittedExpectedItem[0])
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

        private string[] GetSampleDrivers()
        {
            string[] result = new string[]
            {
                "DID#PETMAX,4,Petre,Maxim,Volvo XC 60,Baia Mare,End",
                "DID#SECVIC,4,Second,Victim,Batmobil,string,string",
                "DID#ANDPFE,2,Andreea,Pfeilmayer,BMW X1,Schrozberg,End",
                "DID#HANGRO,2,Hans,Gross,Audi A6,Start,Niederstetten",
                "DID#SENVIV,4,Second,Victim,Old Rusty Bicycle,string,string",
                "DID#EMISOM,4,Emil,Somkereki,BMW X5,Start,Schrozberg",
                "DID#DORHOR,4,Doru,Horincar,Hyundai Tucson,Schrozberg,End",
                "DID#MAXMUS,4,Max,Mustemann,Peugeot 507,Stuttgart,Schrozberg"
            };
            return result;
        }

        private string[] GetSamplePassengers()
        {
            string[] result = new string[]
            {
                "PID#NICSAN,Nicusor,Sandu,Start,End",
                "PID#FELFAR,Felician,Farcas,Schrozberg,Wien",
                "PID#PFE321,Francisc,Pfeilmayer,Start,Niederstetten",
                "PID#ROBPFE,Robert,Pfeilmayer,Start,End",
                "PID#BENFUR,Benjamin,Furo,Start,Malaga",
                "PID#RAUCIC,Raul,Ciceu,Schrozberg,End",
                "PID#PETMAX,Petre,Maxim,Baia Mare,End",
                "PID#FIRVIC,First,Victim,Gotham,City",
                "PID#FIRVWE,First,Victim,string,string",
                "PID#FICVID,First,Victim,string,string",
                "PID#VICEGR,Victor,Egri,Weikersheim,End",
                "PID#MAXMUS,Max,Mustermann,Schrozberg,Berlin"
            };
            return result;
        }
    }
}