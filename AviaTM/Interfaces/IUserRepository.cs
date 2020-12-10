using AviaTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(string id);
        Task AddUser(User user);
        Task<User> UpdateUser(User user);
        Task RemoveUser(string id);
        Task<Customer> GetCustomers(string id);
        Task<Provider> GetProvider(string id);
        Task RemoveCustomer(Customer userCust);
        Task RemoveProvider(Provider userProv);

    }
}
