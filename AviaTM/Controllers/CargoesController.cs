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
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoesController : ControllerBase
    {
        private readonly ICargoRepository _repository;
        public CargoesController(ICargoRepository context) => _repository = context;


        [Authorize(Roles = "admin,customer")]
        [HttpGet("/getcargoval/{id}")]
        public async Task<ActionResult<object>> GetCarg(string id)
        {
            Cargo cargo = await _repository.GetCargo(id);
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
        public async Task<ActionResult<List<Cargo>>> GetCargo()
        {
            if (User.IsInRole("admin"))
            {
                return await _repository.GetCagroWhereAdmin();
            }
            else
            {
                return await _repository.GetCagroWhereUser(User);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _repository.GetCargoId(id);

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
            var rd = await _repository.GetRD(cargo.Id);
            var or = await _repository.GetOrder(cargo.Id);
            
            if (rd != null || or !=null)
            {
                var response = new
                {
                    succes = false,
                    message = "Cannot be edited because the product is in the request :("
                };
                return response;
            }
            var car = await _repository.GetCargo(cargo.Name);
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
                return await _repository.UpdateCargo(cargo);
            }
           
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostCargo(CargoBody cargoBody)
        {

            Customer customer = await _repository.GetCustomer(cargoBody.CustomerEmail);
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
            var carg = await _repository.GetCargo(cargoBody.Name);
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
                await _repository.AddCargo(cargo);
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
            var cargo = await _repository.GetCargoId(id);
            if (cargo == null)
            {
                return NotFound();
            }

            await _repository.RemoveCagro(cargo);

            return NoContent();
        }
    }
}
