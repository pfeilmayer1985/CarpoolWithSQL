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

        // [Route("api/CarPoolApi/GetCarpools")]
        public async Task<ActionResult<IEnumerable<CarpoolsModelData>>> GetAllCarpools()
        {
            List<CarpoolsModelData> items = _carpoolsBusinessService.ListAllCarpoolsBusinessService();
            return items;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [Route("api/CarPoolApi/GetCarpoolById/{id}")]
        public async Task<ActionResult<CarpoolsModelDto>> GetCarpoolById(int id)
        {
            CarpoolsModelDto carpool = _carpoolsBusinessService.ListOneCarpoolByIdBusinessService(id);
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
        //[Route("api/CarPoolApi/AddCarpool")]
        public async Task<ActionResult<CarpoolsModelData>> AddCarpool(int userID, bool wantToDrive, CarpoolsModelData carpool)
        {
            CarpoolsModelData newCarpool = _carpoolsBusinessService.AddCarpoolBusineeService(userID, wantToDrive, carpool);
            return newCarpool;
        }

        /*
        [HttpDelete("{idCarpool}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeleteCarpoolById")]
        public async Task<IActionResult> DeleteCarpool(string idCarpool)
        {
            var item = _carpoolBusinessService.DeleteCarpoolByDriverIdBu(idCarpool.ToUpper());

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _carpoolBusinessService.DeleteCarpoolByDriverIdBu(idCarpool.ToUpper());
                return NoContent();
            }
        }

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
        }

        */
    }
}