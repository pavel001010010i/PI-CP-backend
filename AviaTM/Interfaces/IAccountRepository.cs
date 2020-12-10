using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTM.Controllers;
using AviaTM.Models;

namespace AviaTM.Repository
{
    public interface IAccountRepository
    {
        object GetToken(UserBody user);

        Task<User> GetUser(CustomerBody userBody);
        Task<RequestUser> GetRequestUser(CustomerBody userBody);
        Task<Customer> GetCustomers(CustomerBody userBody);
        Task<Provider> GetProvider(CustomerBody userBody);
        Task<User> GetUserP(ProviderBody providerBody);
        Task<RequestUser> GetRequestUserP(ProviderBody providerBody);
        Task<Customer> GetCustomersP(ProviderBody providerBody);
        Task<Provider> GetProviderP(ProviderBody providerBody);
        Task AddCustomer(Customer customer);
        Task AddUser(User user);
        Task AddProvider(Provider provider);
        Task AddRequestUser(RequestUser requestUser);

    }
}
