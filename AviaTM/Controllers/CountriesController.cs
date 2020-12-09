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
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("/getcountryval/{id}")]
        public async Task<ActionResult<object>> GetCountr(string id)
        {
            Country country = await _context.Country.FirstOrDefaultAsync(x => x.NameCountry == id);
            var response = new
            {
                Latitude = country.Latitude,
                Longitude = country.Longitude
                //  distance = country.Distance
            };
            return response;
        }
        // GET: api/Countries
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
            return await _context.Country.ToListAsync();
        }

        // GET: api/Countries/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(string id)
        {
            var country = await _context.Country.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(string id, Country country)
        {
            if (id != country.NameCountry)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<object>> PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                var response = new
                {
                    succes = false,
                    message = "Model not valid"
                };
                return response;
            }
            _context.Country.Add(country);
            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = true,
                    message = "Country added"
                };
                return response;
            }
            catch (DbUpdateException)
            {
                if (CountryExists(country.NameCountry))
                {
                    var response = new
                    {
                        succes = false,
                        message = "Oops :("
                    };
                    return response;
                    
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountry", new { id = country.NameCountry }, country);
        }

        // DELETE: api/Countries/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(string id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(string id)
        {
            return _context.Country.Any(e => e.NameCountry == id);
        }
    }
}
