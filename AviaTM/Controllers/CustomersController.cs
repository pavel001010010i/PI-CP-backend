using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            var cust = await _context.Customers.ToListAsync();
            var user = _context.User.Where(x => x.LockoutEnable == false).ToList();
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
                return await _context.Customers.Where(x => x.Email == User.Identity.Name).ToListAsync();
            }
            
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

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
            if (id != customer.Id)
            {
                var response = new
                {
                    exist = false,
                    message = "User not adding!"
                };
                return response;
            }
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    exist = true,
                    message = "User update!"
                };
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
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
            var userCust = await _context.Customers.FirstOrDefaultAsync(x => x.Email.Contains(customerBody.Email));
            var user1 = await _context.User.FirstOrDefaultAsync(x => x.Login.Contains(customerBody.Email));
            if (userCust == null&& user1== null)
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
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
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
