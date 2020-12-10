using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _repository;

        public CustomersController(ICustomersRepository context) => _repository = context;

        // GET: api/Customers
        [HttpGet]

        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            var cust = await _repository.GetCustomers();
            var user = _repository.GetUserWhere();
            if (User.IsInRole("admin"))
            {
                foreach(var i in cust)
                {
                    foreach(var j in user)
                    {
                        if(i.Email == j.Login)
                        {
                            customers.Add(i);
                        }
                    }
                }
                return customers;
            }
            else
            {
                return await _repository.GetCustomersWhere(User);
            }
            
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _repository.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutCustomer(int id, Customer customer)
        {
            var existingCustomer = await _repository.GetCustomer(customer.Id);
            if (existingCustomer == null)
            {
                var respons = new
                {
                    exist = false,
                    message = "User not update!"
                };
                return respons;
            }
            var response = await _repository.UpdateCustomer(customer);
            return response;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/Customers")]
        public async Task<ActionResult<object>> PostCustomer([FromBody]CustomerBody customerBody)
        {
            Customer customer = new Customer()
            {
                FirstName = customerBody.FirstName,
                LastName = customerBody.LastName,
                Email = customerBody.Email,
                Age = customerBody.Age,
                PassportData = customerBody.PassportData,
                Sex = customerBody.Sex

            };
            User user = new User()
            {
                Login = customerBody.Email,
                Password = customerBody.Password,
                Role = customerBody.Role,
                LockoutEnable = true
            };
            var userCust = await _repository.GetCustomer(customerBody);
            var user1 = await _repository.GetUser(customerBody);
            if (userCust == null&& user1== null)
            {
                await _repository.AddUser(user);
                await _repository.AddCustomer(customer);
                var response = new
                {
                    exist = true,
                    message = "User adding"
                };
                return response;
            }
            else
            {
                var response = new
                {
                    exist = false,
                    message = "A user with these logins exists"
                };
                return response;
            }

        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _repository.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _repository.RemoveCustomer(customer);

            return NoContent();
        }
    }
}
