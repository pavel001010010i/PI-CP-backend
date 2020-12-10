using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;
using Microsoft.AspNetCore.Authorization;
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderRepository _repository;

        public ProvidersController(IProviderRepository context) => _repository = context;

        [Authorize(Roles ="admin,customer")]
        [HttpPost("/api/Providers/getProv")]
        public async Task<ActionResult<List<Provider>>> PostProviderForRD([FromBody] ProviderRD providerRD)
        {
            var plane = _repository.PostProviderForRD(providerRD);
            List<Provider> list = new List<Provider>();
            foreach(var i in plane)
            {
                list.Add(i.Provider);
            }
            return list;
        }

        [Authorize(Roles ="admin,provider")]
        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<List<Provider>>> GetProvider()
        {
            List<Provider> providers = new List<Provider>();
            var prov = await _repository.GetProviders();
            var user = _repository.GetUserWhere();
            if (User.IsInRole("admin"))
            {
                foreach (var i in prov)
                {
                    foreach (var j in user)
                    {
                        if (i.Email == j.Login)
                        {
                            providers.Add(i);
                        }
                    }
                }
                return providers;
            }
            else
            {
                return await _repository.GetProvoderWhere(User);
            }

        }

        // GET: api/Providers/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _repository.GetProvoder(id);

            if (provider == null)
            {
                return NotFound();
            }
            return provider;
        }

        // PUT: api/Providers/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutProvider(int id, Provider provider)
        {
            var existingProvider= await _repository.GetProvoder(provider.ProviderId);
            if (existingProvider == null)
            {
                var respons = new
                {
                    exist = false,
                    message = "User not update!"
                };
                return respons;
            }
            var response = await _repository.UpdateProvider(provider);
            return response;
        }


                // POST: api/Providers
        [Authorize(Roles = "admin")]
        [HttpPost("/api/Providers")]
        public async Task<ActionResult<object>> PostProvider([FromBody]ProviderBody providerBody)
        {
            Provider provider = new Provider()
            {
                NameCompany = providerBody.NameCompany,
                LicenceNumber = providerBody.LicenceNumber,
                Email = providerBody.Email,
                PhoneNumber = providerBody.PhoneNumber,
                CountresProvider = providerBody.CountresProvider
            };
            User user = new User()
            {
                Login = providerBody.Email,
                Password = providerBody.Password,
                Role = providerBody.Role,
                LockoutEnable = true
            };
            var userProv = await _repository.GetProvider(providerBody);
            var user1 = await _repository.GetUser(providerBody);
            if (userProv == null && user1 == null)
            {
                await _repository.AddUser(user);
                await _repository.AddProvider(provider);
                var response = new
                {
                    exist = true,
                    message = "User adding"
                };
                return response;
            }
            else
            {
                var response = new
                {
                    exist = false,
                    message = "A user with these logins exists"
                };
                return response;
            }
        }

        // DELETE: api/Providers/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var provider = await _repository.GetProvoder(id);
            if (provider == null)
            {
                return NotFound();
            }

            await _repository.RemoveProvider(provider);

            return NoContent();
        }

    }
}
