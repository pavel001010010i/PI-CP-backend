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

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class ProviderRD
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public int Depth { get; set; }
            public int Weight { get; set; }
            public string CountryTo { get; set; }
            public string CountryFrom { get; set; }
        }
        [Authorize(Roles ="admin,customer")]
        [HttpPost("/api/Providers/getProv")]
        public async Task<ActionResult<object>> PostProviderForRD([FromBody] ProviderRD providerRD)
        {
            var plane =  _context.Plane.Include(p=>p.Provider).Where(x =>
                 x.depth >= providerRD.Depth &&
                 x.Height >= providerRD.Height &&
                 x.Width >= providerRD.Width &&
                 x.CapacityWeight >= providerRD.Weight&&
                 x.Provider.CountresProvider.Contains(providerRD.CountryTo) && 
                 x.Provider.CountresProvider.Contains(providerRD.CountryFrom));
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
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvider()
        {
            if (User.IsInRole("admin"))
            {
                List<Provider> providers = new List<Provider>();
                var prov = await _context.Provider.ToListAsync();
                var user = _context.User.Where(x => x.LockoutEnable == false).ToList();
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
                return await _context.Provider.ToListAsync();
            }
            else
            {
                return await _context.Provider.Where(x=> x.Email== User.Identity.Name).ToListAsync();
            }

        }

        // GET: api/Providers/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            var provider = await _context.Provider.FindAsync(id);

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
            if (id != provider.ProviderId)
            {
                var response = new
                {
                    exist = false,
                    message = "User not adding!"
                };
                return response;
            }
            _context.Entry(provider).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    exist = true,
                    message = "User update!"
                };
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
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
            var userProv = await _context.Provider.FirstOrDefaultAsync(x => x.Email.Contains(providerBody.Email));
            var user1 = await _context.User.FirstOrDefaultAsync(x => x.Login.Contains(providerBody.Email));
            if (userProv == null && user1 == null)
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                await _context.Provider.AddAsync(provider);
                await _context.SaveChangesAsync();
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
            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.ProviderId == id);
        }
    }
}
