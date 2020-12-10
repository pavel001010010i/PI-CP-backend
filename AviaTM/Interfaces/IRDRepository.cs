using AviaTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AviaTM.Interfaces
{
    public interface IRDRepository
    {
        Task<int> GetCustomerId(string email);
        Task<int> GetCargoId(string name);
        Task<RequestDelivery> GetRD(int cargoId);
        Task<RequestDelivery> GetRDOriginal(int id);
        Task<Order> GetOrder(int cargoId);
        Task AddRD(RequestDelivery requestDelivery);
        Task<List<RequestDelivery>> GetRDAdmin();
        Task<List<RequestDelivery>> GetRDCustomer(ClaimsPrincipal User);
        Task<List<RequestDelivery>> GetRDProvider(ClaimsPrincipal User);
        Task<object> UpdateRD(RequestDelivery requestDelivery);
        Task<object> RemoveRD(RequestDelivery request);
        /*Task<List<RequestUser>> GetRUsers();
        Task<RequestUser> GetRequestUser(string login);
        
        Task AddUser(RequestUser requestUser);
        Task RemoveRU(RequestUser requestUser);
        Task RemoveCustomer(Customer customer);
        Task RemoveProvider(Provider provider);
        Task<Customer> GetCustomer(string email);
        Task<Provider> GetProvider(string email);*/
    }
}
