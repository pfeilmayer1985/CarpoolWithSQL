using Moq;

namespace TecAlliance.Carpool.Business.Tests
{
    [TestClass]
    public class UserBusinessServiceTests
    {
      //  PassengerBusinessService _passengerBusinessService = new PassengerBusinessService();
        
        private readonly PassengerBusinessService _sysUnderTest;
        private readonly Mock<IPassengerBusinessService> _passengerBusinessServMock = new Mock<IPassengerBusinessService>();
        
        public UserBusinessServiceTests()
        {
            _sysUnderTest = new PassengerBusinessService();
        }
       
        
        [TestMethod]
        public void ListAllPassengersService_ShouldHaveTheTestPassengerInTheFile()
        {
            // Arrange

            // Act

            // Assert


        }


    }
}