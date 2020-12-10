using AviaTM.Interfaces;
using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.Repository
{
    public class UserRepository: IUserRepository
    {
        public ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUser(string id)
        {
            return await _context.User
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Login == id);
        }

        public async Task AddUser(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task RemoveUser(string id)
        {
            var removableUser = await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == id);
            if (removableUser != null)
            {
                _context.Remove(removableUser);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomers(string id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email.Contains(id));
        }

        public async Task<Provider> GetProvider(string id)
        {
            return await _context.Provider.FirstOrDefaultAsync(x => x.Email.Contains(id));
        }

        public async Task RemoveCustomer(Customer userCust)
        {
            _context.Customers.Remove(userCust);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProvider(Provider userProv)
        {
            _context.Provider.Remove(userProv);
            await _context.SaveChangesAsync();
        }
    }
}
