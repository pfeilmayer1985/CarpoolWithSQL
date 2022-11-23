using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private IUserBusinessService _newUserBusinessService;

        public UsersController(ILogger<UsersController> logger, IUserBusinessService newUserBusinessService)
        {
            StringBuilder test = new StringBuilder();
            _newUserBusinessService = newUserBusinessService;
            _logger = logger;
        }

        [HttpGet]
        //[Route("api/CarPoolApi/GetUsers")]
        public async Task<ActionResult<IEnumerable<UserBaseModelData>>> GetAllUsers()
        {
            List<UserBaseModelData> items = _newUserBusinessService.ListAllUserData();
            return items;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<UserBaseModelDto>> GetUserById(int id)
        {
            UserBaseModelDto item = _newUserBusinessService.ListUserDataById(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }

        [HttpPost]
        //[Route("api/CarPoolApi/AddUser")]
        public async Task<ActionResult<UserBaseModelData>> AddUser(UserBaseModelData user)
        {
            UserBaseModelData item = _newUserBusinessService.AddUserBusineeService(user);
            return item;
        }

        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdateUser(int id, string password, UserBaseModelData user)
        {
            var item = _newUserBusinessService.EditUserBusinessService(id, password, user);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.EditUserBusinessService(id, password, user);
                return NoContent();
            }
        }

        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeletePassengerByEmail")]
        public async Task<IActionResult> DeleteUser(int id, string password)
        {
            var item = _newUserBusinessService.DeleteUserBusinessService(id, password);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.DeleteUserBusinessService(id, password);
                return NoContent();
            }
        }
    }
}