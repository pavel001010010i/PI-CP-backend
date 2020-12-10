using AviaTM.Interfaces;
using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.Repository
{
    public class CountryRepository: ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> AddCountry(Country country)
        {
            _context.Country.Add(country);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Country added"
            };
            return response;
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Country.ToListAsync();
        }

        public async Task<Country> GetCountry(string name)=> await _context.Country.AsNoTracking()
                .SingleOrDefaultAsync(x => x.NameCountry == name);

        public async Task<object> RemoveCountry(Country country)
        {
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Country deleted!"
            };
            return response;
        }

        public async Task<object> UpdateCountry(Country country)
        {
            _context.Country.Update(country);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Country updated!"
            };
            return response;
        }
    }
}
