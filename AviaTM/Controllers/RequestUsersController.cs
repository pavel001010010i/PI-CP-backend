using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestUsersController : ControllerBase
    {
        private readonly IRequestUserRepository _repository;
        public RequestUsersController(IRequestUserRepository context) => _repository = context;
        // GET: api/RequestUsers
        [HttpGet]
        public async Task<ActionResult<List<RequestUser>>> GetRequestUser()
        {
            return await _repository.GetRUsers();
        }

        // GET: api/RequestUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestUser>> GetRequestUser(string id)
        {
            var requestUser = await _repository.GetRequestUser(id);
            if (requestUser == null)
            {
                return NotFound();
            }
            return requestUser;
        }

        // PUT: api/RequestUsers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutRequestUser(string id, RequestUser requestUser)
        {
            var planeEx = await _repository.GetRequestUser(requestUser.Login);
            if (planeEx == null)
            {
                return BadRequest();
            }
            return await _repository.UpdateRUser(requestUser);
        }

        // POST: api/RequestUsers
        [HttpPost]
        public async Task<ActionResult<object>> PostRequestUser(RequestUser requestUser)
        {
            await _repository.AddUser(requestUser);
            await _repository.RemoveRU(requestUser);
            var response = new
            {
                succes = true,
                message = "Application form completed"
            };
            return response;
        }

        // DELETE: api/RequestUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteRequestUser(string id)
        {
            var requestUser = await _repository.GetRequestUser(id);
            var userProv = await _repository.GetProvider(id);
            var userCust = await _repository.GetCustomer(id);
            if (userCust != null)
            {
                await _repository.RemoveRU(requestUser);
                await _repository.RemoveCustomer(userCust);
                var response = new
                {
                    succes = false,
                    message = "Application rejected"
                };
                return response;

            }
            if (userProv != null)
            {
                await _repository.RemoveRU(requestUser);
                await _repository.RemoveProvider(userProv);
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

    }
}
