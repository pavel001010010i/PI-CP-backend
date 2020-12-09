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
    public class CargoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CargoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,customer")]
        [HttpGet("/getcargoval/{id}")]
        public async Task<ActionResult<object>> GetCarg(string id)
        {
            Cargo cargo = await _context.Cargo.FirstOrDefaultAsync(x => x.Name == id);
            var response = new
            {
                height = cargo.Height,
                width = cargo.Width,
                depth = cargo.Depth,
                weight = cargo.Weight
            };
            return response;
        }
        // GET: api/Cargoes
        [Authorize(Roles = "admin,customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargo()
        {
            if (User.IsInRole("admin"))
            {
                var cargos = _context.Cargo.Include(p => p.Customer);
                return await cargos.ToListAsync();
            }
            else
            {
                var cargos = _context.Cargo.Include(p => p.Customer).Where(x => x.Customer.Email == User.Identity.Name);
                return await cargos.ToListAsync();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            return cargo;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutCargo(int id, Cargo cargo)
        {
            if (id != cargo.Id)
            {
                return BadRequest();
            }
            var rd = _context.RequestDeliveries.FirstOrDefault(x => x.CargoId == id);
            var or = _context.Order.FirstOrDefault(x => x.CargoId == id);
            if (rd != null || or !=null)
            {
                var response = new
                {
                    succes = false,
                    message = "Cannot be edited because the product is in the request :("
                };
                return response;
            }
            var car = await _context.Cargo.FirstOrDefaultAsync(x => x.Name == cargo.Name);
            if (car != null)
            {
                var response = new
                {
                    succes = false,
                    message = "Сargo with this name already exists"
                };
                return response;
            }
            else
            {
                _context.Entry(cargo).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = true,
                    message = "!The product has been altered!"
                };
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostCargo(CargoBody cargoBody)
        {

            Customer customer = _context.Customers.Where(x => x.Email == cargoBody.CustomerEmail).FirstOrDefault();
            Customer newCust = customer;
            customer = null;
            Cargo cargo= new Cargo()
            {
                Name = cargoBody.Name,
                Height = cargoBody.Height,
                Width = cargoBody.Width,
                Depth = cargoBody.Depth,
                CustomerId = newCust.Id,
                Weight = cargoBody.Weight
            };
            var carg = await _context.Cargo.FirstOrDefaultAsync(x => x.Name == cargoBody.Name);
            if (carg != null)
            {
                var response = new
                {
                    succes = false,
                    message = "This cargo already exists"
                };
                return response;
            }
            else
            {
                _context.Cargo.Add(cargo);
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = true,
                    message = "Cargo add"
                };
                return response;
            }
           
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargoExists(int id)
        {
            return _context.Cargo.Any(e => e.Id == id);
        }
    }
}
