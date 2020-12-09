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
using AviaTM.methods;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestDeliveriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public RequestDeliveriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost("/getDis")]
        public async Task<ActionResult<object>> GetDistance([FromBody]Coordinates coordinates)
        {
            GetDistance getDistance = new GetDistance();

            return getDistance.DistanceEarth(coordinates.X1, coordinates.Y1, coordinates.X2, coordinates.Y2);
        }


        [Authorize]
        [HttpPost("/addrd")]
        public async Task<ActionResult<object>> PostRDAdd(RDBody rd)
        {
            if (User.IsInRole("admin"))
            {
                var response = new
                {
                    succes = false,
                    message = "Administrator can't add requests"
                };
                return response;
            }
            var customerId = _context.Customers.FirstOrDefault(x => x.Email == rd.CustomerEmail).Id;
            var cargoId = _context.Cargo.FirstOrDefault(x => x.Name == rd.CargoName).Id;
            var isCargoExistForRD = _context.RequestDeliveries.FirstOrDefault(x => x.CargoId == cargoId);
            if (isCargoExistForRD!=null)
            {
                var response = new
                {
                    succes = false,
                    message = "!The request for delivery of this product already exists!"
                };
                return response;
            }
            var isCargoExistOrder = await _context.Order.FirstOrDefaultAsync(x => x.CargoId == cargoId);

            if (isCargoExistOrder != null)
            {
                var response = new
                {
                    succes = false,
                    message = "!This product has already been accepted by the supplier company!"
                };
                return response;
            }
            else
            {
                RequestDelivery request = new RequestDelivery()
                {
                    ProviderId = rd.ProviderId,
                    CustomerId = customerId,
                    CargoId = cargoId,
                    CountryIdFrom = rd.CountryNameFrom,
                    CountryIdTo = rd.CountryNameTo,
                    DateDeparture = rd.dateDep,
                    DateDelivery = rd.dateDel,
                    CastDelivery = rd.castDep,
                    StatusRequest = true
                };

                _context.RequestDeliveries.Add(request);
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = true,
                    message = "Application form completed"
                };
                return response;

            }

         
        }
        // GET: api/RequestDeliveries
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestDelivery>>> GetRequestDeliveries()
        {
            if (User.IsInRole("admin"))
            {
                return await _context.RequestDeliveries
                    .Include(x => x.Customer)
                    .Include(x =>x.Provider)
                    .Include(x=>x.Cargo)
                    .Include(x=>x.Country).ToListAsync();
            }
            if (User.IsInRole("provider"))
            {
                return await _context.RequestDeliveries
                      .Include(x => x.Customer)
                      .Include(x => x.Provider).Where(x => x.Provider.Email == User.Identity.Name)
                      .Include(x => x.Cargo)
                      .Include(x => x.Country).ToListAsync();

            }
            return await _context.RequestDeliveries
                    .Include(x => x.Customer).Where(x => x.Customer.Email == User.Identity.Name)
                    .Include(x => x.Provider)
                    .Include(x => x.Cargo)
                    .Include(x => x.Country).ToListAsync();
        }

        // GET: api/RequestDeliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDelivery>> GetRequestDelivery(int id)
        {
            var requestDelivery = await _context.RequestDeliveries.FindAsync(id);

            if (requestDelivery == null)
            {
                return NotFound();
            }

            return requestDelivery;
        }

        // PUT: api/RequestDeliveries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestDelivery(int id, RequestDelivery requestDelivery)
        {
            if (id != requestDelivery.IdRequest)
            {
                return BadRequest();
            }

            _context.Entry(requestDelivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestDeliveryExists(id))
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

        // POST: api/RequestDeliveries
        [HttpPost]
        public async Task<ActionResult<RequestDelivery>> PostRequestDelivery(RequestDelivery requestDelivery)
        {
            _context.RequestDeliveries.Add(requestDelivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestDelivery", new { id = requestDelivery.IdRequest }, requestDelivery);
        }

        // DELETE: api/RequestDeliveries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestDelivery(int id)
        {
            var requestDelivery = await _context.RequestDeliveries.FindAsync(id);
            if (requestDelivery == null)
            {
                return NotFound();
            }

            _context.RequestDeliveries.Remove(requestDelivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestDeliveryExists(int id)
        {
            return _context.RequestDeliveries.Any(e => e.IdRequest == id);
        }
    }
}
