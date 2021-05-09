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
using AviaTM.DB.Model.Models;
using Constant;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeTransportController : ControllerBase
    {
        private readonly ITypeTransportControllerService _context;
        public TypeTransportController(ITypeTransportControllerService context) => _context = context;

        [HttpGet("get")]
        //[Authorize]
        public async Task<IActionResult> GetTypeTransport()
        {
            var typeCargo = await _context.GetTypeTransports();
            if (typeCargo == null)
            {
                return BadRequest();
            }
            return Ok(typeCargo);
        }
        [HttpGet("get-type-transport")]
       // [Authorize(Roles = AspNetRoles.Admin)]
        public async Task<IActionResult> Get()
        {
            var type = await _context.GetTypeTransportsForAdmin();
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }

        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetTypeTransport(int id)
        {
            var type = await _context.GetTypeTransportId(id);
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }
        [HttpPut("update")]
        [Authorize(Roles = AspNetRoles.Admin)]
        public async Task<IActionResult> Update([FromBody] TypeTransport model)
        {
            await _context.Update(model);
            return Ok();
        }
    }
}
