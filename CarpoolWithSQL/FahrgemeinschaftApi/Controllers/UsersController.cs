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
        private IUserBusinessService _userBusinessService;
        private ICarpoolsBusinessService _carpoolsBusinessService;

        public UsersController(ILogger<UsersController> logger, IUserBusinessService userBusinessService, ICarpoolsBusinessService carpoolsBusinessService)
        {
            StringBuilder test = new StringBuilder();
            _userBusinessService = userBusinessService;
            _carpoolsBusinessService = carpoolsBusinessService;

            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserBaseModelData>>> GetAllUsers()
        {
            List<UserBaseModelData> items = _userBusinessService.ListAllUserData();
            return items;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("GetUserByID{userID}")]
        public async Task<ActionResult<UserBaseModelDto>> GetUserById(int userID)
        {
            UserBaseModelDto item = _userBusinessService.ListUserDataById(userID);

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
            UserBaseModelData item = _userBusinessService.AddUserBusineeService(user);
            return item;
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("EditUserById{userID}")]
        public async Task<IActionResult> UpdateUser(int userID, string password, UserBaseModelData user)
        {
            var item = _userBusinessService.EditUserBusinessService(userID, password, user);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _userBusinessService.EditUserBusinessService(userID, password, user);
                return NoContent();
            }
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("DeleteUserByID{userID}")]
        public async Task<IActionResult> DeleteUser(int userID, string password)
        {
            var item = _userBusinessService.ListUserDataById(userID);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _userBusinessService.DeleteUserBusinessService(userID, password);
                return NoContent();
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("RemoveUserFromCarpoolID{carpoolID},{userID}")]
        public async Task<IActionResult> RemoveUserFromCarpool(int carpoolID, int userID, string password)
        {
            var user = _userBusinessService.ListUserDataById(userID);
            var carpool = _carpoolsBusinessService.ListOneCarpoolByIdBusinessService(carpoolID);

            if (user == null || carpool == null)
            {
                return NotFound();
            }
            else
            {
                _userBusinessService.DeleteUserBusinessService(userID, password);
                return NoContent();
            }
        }
    }
}