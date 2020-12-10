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
    public class OrderRepository: IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomer(ClaimsPrincipal User)
        {
            return  _context.Customers.FirstOrDefault(x => x.Email == User.Identity.Name);
        }

        public async Task<Order> GetOrder(int id) => await _context.Order.FindAsync(id);

        public async Task<List<Order>> GetOrderAdmin()
        {
            return await _context.Order
                    .Include(x => x.Customer)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country)
                    .Include(x => x.Plane).ToListAsync();
        }

        public async Task<List<Order>> GetOrderCustomer(ClaimsPrincipal User)
        {
            return await _context.Order
                    .Include(x => x.Customer).Where(x => x.Customer.Email == User.Identity.Name)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country)
                    .Include(x => x.Plane).ToListAsync();
        }

        public async Task<List<Order>> GetOrderProvider(ClaimsPrincipal User)
        {
            return await _context.Order
                      .Include(x => x.Customer)
                      .Include(x => x.Provider).Where(x => x.Provider.Email == User.Identity.Name)
                      .Include(x => x.Cargo)
                      .Include(x => x.Country)
                      .Include(x => x.Plane).ToListAsync();
        }

        public async Task<Order> GetOrderWhere(int id) => await _context.Order.Where(x => x.CustomerId == id).FirstOrDefaultAsync();

        public async Task<Order> GetOrderWhereP(int id)=> await _context.Order.Where(x => x.ProviderId == id).FirstOrDefaultAsync();

        public async Task<List<Plane>> GetPlane(RequestDelivery requestDelivery)
        {
            return await _context.Plane.Where(x => x.IdProvider == requestDelivery.ProviderId).ToListAsync();
        }

        public async Task<Provider> GetProvider(ClaimsPrincipal User)
        {
            return _context.Provider.FirstOrDefault(x => x.Email == User.Identity.Name);
        }

        public async Task<RequestDelivery> GetRD(int idRequest) => await _context.RequestDeliveries.FindAsync(idRequest);

        public async Task RemoveOrder(Order order)
        {
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRD(RequestDelivery requestDelivery)
        {
            _context.RequestDeliveries.Remove(requestDelivery);
            await _context.SaveChangesAsync();
        }
    }
}
