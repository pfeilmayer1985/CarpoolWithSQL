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
        [Route("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserBaseModelData>>> GetAllUsers()
        {
            List<UserBaseModelData> items = _newUserBusinessService.ListAllUserData();
            return items;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetUserByID{userID}")]
        public async Task<ActionResult<UserBaseModelDto>> GetUserById(int userID)
        {
            UserBaseModelDto item = _newUserBusinessService.ListUserDataById(userID);

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
        [Route("AddUser")]
        public async Task<ActionResult<UserBaseModelData>> AddUser(UserBaseModelData user)
        {
            UserBaseModelData item = _newUserBusinessService.AddUserBusineeService(user);
            return item;
        }

        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("EditUserById{userID}")]
        public async Task<IActionResult> UpdateUser(int userID, string password, UserBaseModelData user)
        {
            var item = _newUserBusinessService.EditUserBusinessService(userID, password, user);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.EditUserBusinessService(userID, password, user);
                return NoContent();
            }
        }

        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("DeletePassengerByID{userID}")]
        public async Task<IActionResult> DeleteUser(int userID, string password)
        {
            var item = _newUserBusinessService.DeleteUserBusinessService(userID, password);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.DeleteUserBusinessService(userID, password);
                return NoContent();
            }
        }
    }
}