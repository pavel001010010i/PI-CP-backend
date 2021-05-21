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
    public class TransportController : ControllerBase
    {
        private readonly ITransportControllerService _context;

        public TransportController(ITransportControllerService context) => _context = context;

        [HttpPost("search-transport")]
        public async Task<IActionResult> Search([FromBody] SearchtModel model)
        {
            var transports = await _context.SearchTransport(model);

            return Ok(transports);
        }


        [HttpPost("add-transport")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] RouteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model == null)
            {
                return BadRequest();
            }

            await _context.AddTransport(model);
            return Ok();
        }

        [HttpGet("get-transports")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var transports = await _context.GetTransports();
            return Ok(transports);
        }
        [HttpPut("update-transport")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] RouteModel model)
        {
            await _context.UpdateTransport(model);
            return Ok();
        }

        [HttpDelete("delete-transport/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.DeleteTransport(id);
            return Ok(message);
        }
    }
}
