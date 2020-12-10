using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;
using AviaTM.Repository;
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repository;

        public UsersController(IUserRepository context) => _repository = context;

        // GET: api/Users
        [HttpGet]
        public async Task<List<User>> GetUser()
        {
            return await _repository.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            return await _repository.GetUser(id);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            var existingUser = await _repository.GetUser(user.Login);
            if (existingUser == null)
            {
                return BadRequest("User not updated");
            }

            await _repository.UpdateUser(user);

            return Ok("User updated");
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _repository.AddUser(user);
            return Ok("User added");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user =  await _repository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            var userProv = await _repository.GetProvider(id);
            var userCust = await _repository.GetCustomers(id);
            if(userProv!=null)
            {
                await _repository.RemoveProvider(userProv); 
            }
            if (userCust != null)
            {
                await _repository.RemoveCustomer(userCust);
            }
            await _repository.RemoveUser(user.Login);
            return NoContent();
        }
    }
}
