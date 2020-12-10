using AviaTM.Interfaces;
using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.Repository
{
    public class RequestUserRepository: IRequestUserRepository
    {
        private readonly ApplicationDbContext _context;
        public RequestUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(RequestUser requestUser)
        {
            User user = new User() { Login = requestUser.Login, Role = requestUser.Role, Password = requestUser.Password, LockoutEnable = false };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomer(string email) => await _context.Customers.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == email);

        public async Task<Provider> GetProvider(string email)=> await _context.Provider.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email == email);

        public async Task<RequestUser> GetRequestUser(string login) => await _context.RequestUser.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Login == login);

        public async Task<List<RequestUser>> GetRUsers()
        {
            return await _context.RequestUser.ToListAsync();
        }

        public async Task RemoveCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProvider(Provider provider)
        {
            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRU(RequestUser requestUser)
        {
            _context.RequestUser.Remove(requestUser);
            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateRUser(RequestUser requestUser)
        {
            _context.RequestUser.Update(requestUser);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Request User updated!"
            };
            return response;
        }
    }
}
