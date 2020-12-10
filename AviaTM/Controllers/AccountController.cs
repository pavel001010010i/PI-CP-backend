using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AviaTM.Models; // класс Person
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AviaTM.Repository;

namespace AviaTM.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly  IAccountRepository _repository;
        public AccountController(IAccountRepository context) => _repository = context;


        [HttpPost("/token")]
        public async Task<ActionResult<object>> Token([FromBody] UserBody user)
        {
            return _repository.GetToken(user);
        }

        [HttpPost("/addcustomer")]
        public async Task<ActionResult<object>> AddCustomer([FromBody] CustomerBody userBody)
        {
            var userT = await _repository.GetUser(userBody);
            var userReques = await _repository.GetRequestUser(userBody);
            var userCust = await _repository.GetCustomers(userBody);
            var userProv = await _repository.GetProvider(userBody);
            if (!ModelState.IsValid)
            {
                var response = new
                {
                    exist = false,
                    message = "Model not is valid"
                };
                return response;
            }
            if (User.IsInRole("admin"))
            {
                if (userT == null && userCust == null && userProv == null && userReques==null)
                {
                    User user = new User() { Login = userBody.Email, Password = userBody.Password, Role = "customer", LockoutEnable = true };
                    Customer customer = new Customer()
                    {
                        FirstName = userBody.FirstName,
                        LastName = userBody.LastName,
                        Email = userBody.Email,
                        Age = userBody.Age,
                        PassportData = userBody.PassportData,
                        Sex = userBody.Sex
                    };

                    await _repository.AddCustomer(customer);
                    await _repository.AddUser(user);
                    var response = new
                    {
                        exist = true,
                        message = "User added successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }
            }
            else
            {
                if (userT == null && userCust == null && userProv == null && userReques == null)
                {
                    RequestUser ru = new RequestUser { Login = userBody.Email, Password = userBody.Password, Role = "customer"};
                    Customer customer = new Customer()
                    {
                        FirstName = userBody.FirstName,
                        LastName = userBody.LastName,
                        Email = userBody.Email,
                        Age = userBody.Age,
                        PassportData = userBody.PassportData,
                        Sex = userBody.Sex
                    };

                    await _repository.AddCustomer(customer);
                    await _repository.AddRequestUser(ru);
                    var response = new
                    {
                        exist = true,
                        message = "Your request has been sent. Wait for admin approval"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }

            }
            
        }
        [HttpPost("/addprovider")]
        public async Task<ActionResult<object>> AddProvider([FromBody] ProviderBody providerBody)
        {
            var userT = await _repository.GetUserP(providerBody);
            var userReques = await _repository.GetRequestUserP(providerBody);
            var userCust = await _repository.GetCustomersP(providerBody);
            var userProv = await _repository.GetProviderP(providerBody);
            if (!ModelState.IsValid)
            {
                var response = new
                {
                    exist = false,
                    message = "Model not is valid"
                };
                return response;
            }
            if (User.IsInRole("admin"))
            {
                if (userT == null && userCust == null && userProv == null && userReques==null)
                {
                    User user = new User() { Login = providerBody.Email, Password = providerBody.Password, Role = "provider", LockoutEnable = true };
                    Provider provider = new Provider()
                    {
                        NameCompany = providerBody.NameCompany,
                        LicenceNumber = providerBody.LicenceNumber,
                        Email = providerBody.Email,
                        PhoneNumber = providerBody.PhoneNumber,
                        CountresProvider = providerBody.CountresProvider
                    };

                    await _repository.AddProvider(provider);
                    await _repository.AddUser(user);
                    var response = new
                    {
                        exist = true,
                        message = "User added successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }
            }
            else
            {
                if (userT == null && userCust == null && userProv == null && userReques == null)
                {
                    RequestUser ru = new RequestUser { Login = providerBody.Email, Password = providerBody.Password, Role = "provider" };
                    Provider provider = new Provider()
                    {
                        NameCompany = providerBody.NameCompany,
                        LicenceNumber = providerBody.LicenceNumber,
                        Email = providerBody.Email,
                        PhoneNumber = providerBody.PhoneNumber,
                        CountresProvider = providerBody.CountresProvider
                    };

                    await _repository.AddProvider(provider);
                    await _repository.AddRequestUser(ru);
                    var response = new
                    {
                        exist = true,
                        message = "User added successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }
            }
            
        }


        
    }
}