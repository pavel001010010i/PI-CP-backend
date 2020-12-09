using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestUser>>> GetRequestUser()
        {
            return await _context.RequestUser.ToListAsync();
        }

        // GET: api/RequestUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestUser>> GetRequestUser(string id)
        {
            var requestUser = await _context.RequestUser.FindAsync(id);

            if (requestUser == null)
            {
                return NotFound();
            }

            return requestUser;
        }

        // PUT: api/RequestUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestUser(string id, RequestUser requestUser)
        {
            if (id != requestUser.Login)
            {
                return BadRequest();
            }

            _context.Entry(requestUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RequestUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<object>> PostRequestUser(RequestUser requestUser)
        {
            User user = new User() { Login = requestUser.Login, Role = requestUser.Role, Password = requestUser.Password, LockoutEnable = false };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var ru = await _context.RequestUser.FindAsync(requestUser.Login);
            _context.RequestUser.Remove(ru);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Application form completed"
            };
            return response;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RequestUserExists(requestUser.Login))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return response;
        }

        // DELETE: api/RequestUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteRequestUser(string id)
        {
            var requestUser = await _context.RequestUser.FindAsync(id);
            var userProv = await _context.Provider.FirstOrDefaultAsync(x => x.Email == id);
            var userCust = await _context.Customers.FirstOrDefaultAsync(x=>x.Email == id);
            if (userCust != null)
            {
                _context.RequestUser.Remove(requestUser);
                await _context.SaveChangesAsync();
                _context.Customers.Remove(userCust);
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = false,
                    message = "Application rejected"
                };
                return response;

            }
            if (userProv != null)
            {
                _context.RequestUser.Remove(requestUser);
                await _context.SaveChangesAsync();
                _context.Provider.Remove(userProv);
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = false,
                    message = "Application rejected"
                };
                return response;

            }
            var respons = new
            {
                succes = false,
                message = "error"
            };
            return respons;
        }

        private bool RequestUserExists(string id)
        {
            return _context.RequestUser.Any(e => e.Login == id);
        }
    }
}
