using AviaTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AviaTM.Interfaces
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(CustomerBody customerBody);
        Task<User> GetUser(CustomerBody customerBody);
        Task<List<Customer>> GetCustomersWhere(ClaimsPrincipal User);
        List<User> GetUserWhere();
        Task AddCustomer(Customer customer);
        Task AddUser(User user);
        Task<object> UpdateCustomer(Customer customer);
        Task<Customer> GetCustomer(int id);

        Task RemoveCustomer(Customer userCust);
    }
}
