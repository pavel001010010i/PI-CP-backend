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
    public class CargoRepository: ICargoRepository
    {
        private readonly ApplicationDbContext _context;

        public CargoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cargo>> GetCagroWhereAdmin()
        {
            return await _context.Cargo.Include(p => p.Customer).ToListAsync();
        }

        public async Task<List<Cargo>> GetCagroWhereUser(ClaimsPrincipal User)
        {
            return await _context.Cargo.Include(p => p.Customer).Where(x => x.Customer.Email == User.Identity.Name).ToListAsync();
        }

        public async Task<Cargo> GetCargo(string name) => await _context.Cargo.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == name);
        public async Task<Cargo> GetCargoId(int id) => await _context.Cargo.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        public async Task<RequestDelivery> GetRD(int id) => await _context.RequestDeliveries.FirstOrDefaultAsync(x => x.IdRequest == id);
        public async Task<Order> GetOrder(int id) => await _context.Order.FirstOrDefaultAsync(x => x.OrderId == id);

        public async Task<object> UpdateCargo(Cargo cargo)
        {
            _context.Cargo.Update(cargo);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "!The product has been altered!"
            };
            return response;
        }

        public async Task<Customer> GetCustomer(string email) => await _context.Customers.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email == email);

        public async Task AddCargo(Cargo cargo)
        {
            _context.Cargo.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCagro(Cargo cargo)
        {
            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();
        }
    }
}
