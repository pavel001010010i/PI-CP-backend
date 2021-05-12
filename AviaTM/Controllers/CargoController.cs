using AviaTM.DB.Model.Models;
using AviaTM.Services.IServicesController;
using AviaTM.Services.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly ICargoControllerService _context;

        public CargoController(ICargoControllerService context) => _context = context;

        [HttpPost("search-cargo")]
        public async Task<IActionResult> Search([FromBody] SearchtModel model)
        {
            var cargoes = await _context.SearchCargo(model);

            return Ok(cargoes);
        }

        [HttpPost("add-cargo")]
        [Authorize]
        public async Task<IActionResult> AddCargo([FromBody] RouteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest();
            }

            await _context.AddCargos(model);
            return Ok();
        }

        [HttpGet("get-cargo/{id}")]
        [Authorize]
        public async Task<IActionResult> GetCargoId(int id)
        {
            var cargo = await _context.GetCargoId(id);
            if (cargo == null)
            {
                return BadRequest();
            }
            return Ok(cargo);
        }

        [HttpGet("get-cargoes")]
        [Authorize]
        public async Task<IActionResult> GetCargo()
        {
            var cargoes = await _context.GetCargos();
            return Ok(cargoes);
        }

        [HttpGet("get-cargoes-request")]
        [Authorize]
        public async Task<IActionResult> GetCargoesForSelectRequest()
        {
            var cargoes = await _context.GetCargoesForSelectRequest();
            return Ok(cargoes);
        }

        [HttpPut("update-cargo")]
        [Authorize]
        public async Task<IActionResult> UpdateCargo([FromBody] RouteModel model)
        {
            await _context.UpdateCargo(model);
            return Ok();
        }

        [HttpDelete("delete-cargo/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            await _context.DeleteCargo(id);
            return Ok();
        }
    }
}
