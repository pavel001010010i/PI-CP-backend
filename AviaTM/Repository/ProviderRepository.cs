using AviaTM.Interfaces;
using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AviaTM.Repository
{
    public class ProviderRepository: IProviderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProvider(Provider provider)
        {
            await _context.Provider.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Provider> GetProvider(ProviderBody providerBody)
        {
            return await _context.Provider.FirstOrDefaultAsync(x => x.Email == providerBody.Email);
        }

        public async Task<List<Provider>> GetProviders()
        {
            return await  _context.Provider.ToListAsync();
        }

        public async Task<Provider> GetProvoder(int id) => await _context.Provider.AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProviderId == id);


        public async Task<List<Provider>> GetProvoderWhere(ClaimsPrincipal User)
        {
            return await _context.Provider.Where(x => x.Email == User.Identity.Name).ToListAsync();
        }

        public async Task<User> GetUser(ProviderBody providerBody)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Login == providerBody.Email);
        }

        public List<User> GetUserWhere()
        {
            return _context.User.Where(x => x.LockoutEnable == false).ToList();
        }

        public IQueryable<Plane> PostProviderForRD(ProviderRD providerRD)
        {
            return _context.Plane.Include(p => p.Provider).Where(x =>
                   x.depth >= providerRD.Depth &&
                   x.Height >= providerRD.Height &&
                   x.Width >= providerRD.Width &&
                   x.CapacityWeight >= providerRD.Weight &&
                   x.Provider.CountresProvider.Contains(providerRD.CountryTo) &&
                   x.Provider.CountresProvider.Contains(providerRD.CountryFrom));
        }

        public async Task RemoveProvider(Provider provider)
        {
            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateProvider(Provider provider)
        {
            _context.Provider.Update(provider);
            await _context.SaveChangesAsync();
            var response = new
            {
                exist = true,
                message = "User update!"
            };
            return response;
        }
    }
}
