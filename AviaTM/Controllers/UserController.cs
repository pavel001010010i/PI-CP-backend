using Microsoft.AspNetCore.Mvc;
using AviaTM.Interfaces;
using AviaTM.Services.IServicesController;
using System.Threading.Tasks;
using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserControllerService _context;

        public UserController(IUserControllerService context) => _context = context;

        [HttpGet,Route("get-users")]
        [Authorize(Roles = Constant.AspNetRoles.Admin)]
        public async Task<IEnumerable<AppUser>> GetUser()
        {
            return await _context.GetAllUsersByUserRole();
        }

        [HttpGet, Route("update-user/{id}")]
        [Authorize(Roles = Constant.AspNetRoles.Admin)]
        public async Task<IActionResult> UpdateUserLockout(string id)
        {
            var response = await _context.UpdateUserLockout(id);

            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(string id)
        //{
        //    return await _repository.GetUser(id);
        //}

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(string id, User user)
        //{
        //    var existingUser = await _repository.GetUser(user.Login);
        //    if (existingUser == null)
        //    {
        //        return BadRequest("User not updated");
        //    }

        //    await _repository.UpdateUser(user);

        //    return Ok("User updated");
        //}

        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    await _repository.AddUser(user);
        //    return Ok("User added");
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(string id)
        //{
        //    var user =  await _repository.GetUser(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var userProv = await _repository.GetProvider(id);
        //    var userCust = await _repository.GetCustomers(id);
        //    if(userProv!=null)
        //    {
        //        await _repository.RemoveProvider(userProv); 
        //    }
        //    if (userCust != null)
        //    {
        //        await _repository.RemoveCustomer(userCust);
        //    }
        //    await _repository.RemoveUser(user.Login);
        //    return NoContent();
        //}
    }
}
