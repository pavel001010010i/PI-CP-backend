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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _repository;

        public CountriesController(ICountryRepository context) => _repository = context;
        [Authorize]
        [HttpGet("/getcountryval/{id}")]
        public async Task<ActionResult<object>> GetCountr(string id)
        {
            Country country = await _repository.GetCountry(id);
            var response = new
            {
                Latitude = country.Latitude,
                Longitude = country.Longitude
            };
            return response;
        }
        // GET: api/Countries
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountry()
        {
            return await _repository.GetCountries();
        }

        // GET: api/Countries/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(string id)
        {
            var country = await _repository.GetCountry(id);
            if(country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutCountry(string id, Country country)
        {
            var existcountry = await _repository.GetCountry(country.NameCountry);

            if (existcountry == null) 
            {
                return BadRequest();
            }
            return await _repository.UpdateCountry(country);
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
            var countryEx = await _repository.GetCountry(country.NameCountry);
            if (countryEx != null)
            {
                var response = new
                {
                    succes = false,
                    message = "The country already exists"
                };
                return response;
            }
            return await _repository.AddCountry(country);
        }

        // DELETE: api/Countries/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteCountry(string id)
        {
            var country = await _repository.GetCountry(id);
            if (country == null)
            {
                return NotFound();
            }
            return await _repository.RemoveCountry(country);
        }
    }
}
