using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Models;
using Microsoft.AspNetCore.Authorization;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            if (User.IsInRole("admin"))
            {
                return await _context.Order
                    .Include(x => x.Customer)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country)
                    .Include(x => x.Plane).ToListAsync();
            }
            if (User.IsInRole("provider"))
            {
                return await _context.Order
                      .Include(x => x.Customer)
                      .Include(x => x.Provider).Where(x => x.Provider.Email == User.Identity.Name)
                      .Include(x => x.Cargo)
                      .Include(x => x.Country)
                      .Include(x => x.Plane).ToListAsync();

            }
            return await _context.Order
                    .Include(x => x.Customer).Where(x => x.Customer.Email == User.Identity.Name)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country)
                    .Include(x => x.Plane).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("/addorder")]
        public async Task<ActionResult<object>> PostOrderAddFromRD(RequestDelivery delivery)
        {
            try
            {
                var kek = delivery;
                var plane = await _context.Plane.Where(x => x.IdProvider == delivery.ProviderId).ToListAsync();
                Random rand = new Random(DateTime.Now.ToString().GetHashCode());
                int index = rand.Next(0, plane.Count);
                Order order = new Order()
                {
                    CustomerId = delivery.CustomerId,
                    ProviderId = delivery.ProviderId,
                    CargoId = delivery.CargoId,
                    CountryIdTo = delivery.CountryIdTo,
                    CountryIdFrom = delivery.CountryIdFrom,
                    PlaneId = plane[index].Id,
                    DateDeparture = delivery.DateDeparture,
                    DateDelivery = delivery.DateDelivery,
                    Status = "confirmation",
                    CastDelivery = delivery.CastDelivery
                };
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                var del = await _context.RequestDeliveries.FindAsync(delivery.IdRequest);
                _context.RequestDeliveries.Remove(del);
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = true,
                    message = "Order form completed"
                };
                return response;
            }
            catch
            {
                var response = new
                {
                    succes = false,
                    message = "Oops :("
                };
                return response;
            }
           
        }
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteOrder(int id)
        {
            var cust = _context.Customers.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (cust != null)
            {
                var customer = _context.Order.Where(x => x.CustomerId == cust.Id);
                if (customer != null)
                {
                    var order = await _context.Order.FindAsync(id);
                    _context.Order.Remove(order);
                    await _context.SaveChangesAsync();
                    var respons = new
                    {
                        succes = true,
                        message = "Order deleted!"
                    };
                    return respons;

                }
            }
            var prov = _context.Provider.FirstOrDefault(x => x.Email == User.Identity.Name);
            if (prov != null)
            {
                var provider = _context.Order.Where(x => x.ProviderId == prov.ProviderId);
                if (provider != null)
                {
                    var order = await _context.Order.FindAsync(id);
                    _context.Order.Remove(order);
                    await _context.SaveChangesAsync();
                    var respons = new
                    {
                        succes = true,
                        message = "Order deleted!"
                    };
                    return respons;
                }
            }
            if (User.IsInRole("admin")){
                var order = await _context.Order.FindAsync(id);
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
                var respons = new
                {
                    succes = true,
                    message = "Order deleted!"
                };
                return respons;
            }
            var response = new
            {
                succes = false,
                message = "You can't delete it :("
            };
            return response;
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
