using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarpoolsController : ControllerBase
    {
        private readonly ILogger<CarpoolsController> _logger;
        private ICarpoolsBusinessService _carpoolsBusinessService;

        public CarpoolsController(ILogger<CarpoolsController> logger, ICarpoolsBusinessService carpoolsBusinessService)
        {
            _carpoolsBusinessService = carpoolsBusinessService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCarpools")]
        public async Task<ActionResult<IEnumerable<CarpoolsModelData>>> GetAllCarpools()
        {
            List<CarpoolsModelData> items = _carpoolsBusinessService.ListAllCarpoolsBusinessService();
            return items;
        }

        [HttpGet]
        [Route("GetCarpoolById{carpoolID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolsModelDto>> GetCarpoolById(int carpoolID)
        {
            CarpoolsModelDto carpool = _carpoolsBusinessService.ListOneCarpoolByIdBusinessService(carpoolID);
            if (carpool == null)
            {
                return NotFound();
            }
            else
            {
                return carpool;
            }
        }

        [HttpPost]
        [Route("DriverCreatesCarpool{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolsModelData>> AddCarpool(int userID, CarpoolsModelData carpool)
        {
            CarpoolsModelData newCarpool = _carpoolsBusinessService.AddCarpoolBusineeService(userID, carpool);
            return newCarpool;
        }

        [HttpPost]
        [Route("PassengerJoinsExistingCarpool{carpoolID},{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarpoolPassengersModelData>> JoinCarpool(int carpoolID, int userID)
        {
            CarpoolPassengersModelData newPassengerToJoinCarpool = new CarpoolPassengersModelData(carpoolID, userID);
            _carpoolsBusinessService.JoinExistingCarpoolBusineeService(newPassengerToJoinCarpool);
            return newPassengerToJoinCarpool;
        }

        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("DeleteCarpoolById{carpoolID},{userID},{password}")]
        public async Task<IActionResult> DeleteCarpoolByID(int carpoolID, int userID, string password)
        {
            var item = _carpoolsBusinessService.ListOneCarpoolByIdBusinessService(carpoolID);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _carpoolsBusinessService.DeleteCarpoolByCarpoolIDBusinessService(carpoolID, userID, password);
                return NoContent();
            }
        }

        /*
        [HttpDelete("{idDriver}/{idPassenger}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeletePassengerFromCarpool")]
        public async Task<IActionResult> DeletePassengerFromCarpool(string idDriver, string idPassenger)
        {
            var items = _carpoolBusinessService.RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(idDriver.ToUpper(), idPassenger.ToUpper());
            if (items == null)
            {
                return NotFound();
            }
            else
            {
                _carpoolBusinessService.RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(idDriver.ToUpper(), idPassenger.ToUpper());
                return NoContent();
            }
        }*/

        
    }
}