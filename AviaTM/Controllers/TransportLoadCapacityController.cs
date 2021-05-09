using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AviaTM;
using AviaTM.Db.Models;
using Microsoft.AspNetCore.Authorization;
using AviaTM.Interfaces;
using AviaTM.Services.IServicesController;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportLoadCapacityController : ControllerBase
    {
        private readonly ITransportLoadCapacityControllerService _context;
        public TransportLoadCapacityController(ITransportLoadCapacityControllerService context) => _context = context;

        [HttpGet("get")]
        //[Authorize]
        public async Task<IActionResult> GetTypePayment()
        {
            var type = await _context.GetTransportLoadCapacies();
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }

        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetTypePayment(int id)
        {
            var type = await _context.GetTransportLoadCapacityId(id);
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }

    }
}
