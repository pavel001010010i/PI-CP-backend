using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AviaTM.Services.Models.Infastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using AviaTM.Services.IServicesController;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountControllerService _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserContext _userContext;
        public AccountController(IAccountControllerService context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            UserContext userContext)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userContext = userContext;
        }

        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {

            var user = await _context.FindUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        [HttpPost("registration-user")]
        public async Task<IActionResult> RegistrationUser([FromBody] RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var responseMessage = await _context.RegistrationUser(model);

            if (!responseMessage.Status)
            {
                return BadRequest(responseMessage);
            }

            return Ok(responseMessage);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserBody model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var checkedUser = await _context.FindUser(model.Login);
            if (checkedUser == null) 
            {
                return BadRequest();
            }

            if (checkedUser.isLockdown)
            {
                return BadRequest(new ResponseMessageModel
                {
                    Status=false,
                    Message="Упс... Вы заблокированы :("
                });
            }

            bool isPasswordValid = await _context.IsPasswordValid(checkedUser, model.Password);
            if (!isPasswordValid)
            {
                return BadRequest();
            }
            string token = await _context.GenerateToken(checkedUser);

            return Ok(token);
           
        }

        
        
    }
}