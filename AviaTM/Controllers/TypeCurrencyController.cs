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
    public class TypeCurrencyController : ControllerBase
    {
        private readonly ITypeCurrencyControllerService _context;
        public TypeCurrencyController(ITypeCurrencyControllerService context) => _context = context;

        [HttpGet("get")]
        public IActionResult GetTypeCurrency()
        {
            var type = _context.GetTypeCurrencyies();
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }

        [HttpGet("get-type-currency")]
        //[Authorize(Roles = AspNetRoles.Admin)]
        public IActionResult Get()
        {
            var typeCarg = _context.GetTypeForAdmin();
            if (typeCarg == null)
            {
                return BadRequest();
            }
            return Ok(typeCarg);
        }

        [HttpPut("update")]
        [Authorize(Roles = AspNetRoles.Admin)]
        public async Task<IActionResult> UpdateTypeCargo([FromBody] TypeCurrency model)
        {
            await _context.Update(model);
            return Ok();
        }

        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetTypeCurrency(int id)
        {
            var type = await _context.GetTypeCurrencyId(id);
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }
        
    }
}
