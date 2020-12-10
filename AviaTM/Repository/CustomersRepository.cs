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
    public class CustomersRepository : ICustomersRepository
    {
        public ApplicationDbContext _context;
        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddUser(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Customer>> GetCustomers() => await _context.Customers.ToListAsync();

        public async Task<List<Customer>> GetCustomersWhere(ClaimsPrincipal User) => await _context.Customers.Where(x => x.Email == User.Identity.Name).ToListAsync();


        public async Task<Customer> GetCustomer(int id) => await _context.Customers.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

        public List<User> GetUserWhere() => _context.User.Where(x => x.LockoutEnable == false).ToList();

        public async Task RemoveCustomer(Customer userCust)
        {
            _context.Customers.Remove(userCust);
            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            var response = new
            {
                exist = true,
                message = "User update!"
            };
            return response;
        }
        public async Task<Customer> GetCustomer(CustomerBody customerBody) => await _context.Customers.FirstOrDefaultAsync(x => x.Email == customerBody.Email);

        public async Task<User> GetUser(CustomerBody customerBody) => await _context.User.FirstOrDefaultAsync(x => x.Login == customerBody.Email);

        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

    }
}
