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
    public class RouteMapController : ControllerBase
    {
        private readonly IRouteMapControllerService _context;

        public RouteMapController(IRouteMapControllerService context) => _context = context;


        [HttpGet("get-routes")]
        [Authorize]
        public async Task<IActionResult> GetRouteMaps()
        {
            var cargoes = await _context.GetRouteMaps();
            return Ok(cargoes);
        }
        [HttpGet("get-route/{id}")]
        //[Authorize]
        public async Task<IActionResult> GetRoupeMap(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var cargoes = await _context.GetRouteMap(id);
            return Ok(cargoes);
        }
        [HttpPut("update-route")]
        [Authorize]
        public async Task<IActionResult> UpdateCargo([FromBody] CargoModel model)
        {
            //await _context.UpdateCargo(model);
            return Ok();
        }

        [HttpDelete("delete-route/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            //await _context.DeleteCargo(id);
            return Ok();
        }
    }
}
