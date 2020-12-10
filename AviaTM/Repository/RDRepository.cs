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
    public class RDRepository : IRDRepository
    {
        private readonly ApplicationDbContext _context;

        public RDRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCustomerId(string email)
        {
            var cust = await _context.Customers.AsNoTracking()
               .SingleOrDefaultAsync(x => x.Email == email);
            return cust.Id;
        }

        public async Task<int> GetCargoId(string name)
        {
            var cargo = await _context.Cargo.AsNoTracking()
              .SingleOrDefaultAsync(x => x.Name == name);
            return cargo.Id;
        }

        public async Task<RequestDelivery> GetRD(int cargoId) => await _context.RequestDeliveries.AsNoTracking()
            .SingleOrDefaultAsync(x => x.CargoId == cargoId);
        public async Task<RequestDelivery> GetRDOriginal(int id) => await _context.RequestDeliveries.AsNoTracking()
            .SingleOrDefaultAsync(x => x.IdRequest == id);

        public async Task<Order> GetOrder(int cargoId) => await _context.Order.AsNoTracking()
            .SingleOrDefaultAsync(x => x.CargoId == cargoId);

        public async Task AddRD(RequestDelivery requestDelivery)
        {
            _context.RequestDeliveries.Add(requestDelivery);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RequestDelivery>> GetRDAdmin()
        {
            return await _context.RequestDeliveries
                    .Include(x => x.Customer)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country).ToListAsync();
        }

        public async Task<List<RequestDelivery>> GetRDCustomer(ClaimsPrincipal User)
        {
            return await _context.RequestDeliveries
                    .Include(x => x.Customer).Where(x => x.Customer.Email == User.Identity.Name)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country).ToListAsync();
        }

        public async Task<List<RequestDelivery>> GetRDProvider(ClaimsPrincipal User)
        {
            return await _context.RequestDeliveries
                      .Include(x => x.Customer)
                      .Include(x => x.Provider).Where(x => x.Provider.Email == User.Identity.Name)
                      .Include(x => x.Cargo)
                      .Include(x => x.Country).ToListAsync();
        }

        public async Task<object> UpdateRD(RequestDelivery requestDelivery)
        {
            _context.RequestDeliveries.Update(requestDelivery);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Request delivery updated!"
            };
            return response;
        }

        public async Task<object> RemoveRD(RequestDelivery request)
        {
            _context.RequestDeliveries.Remove(request);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Request delivery deleted!"
            };
            return response;
        }
    }
}
