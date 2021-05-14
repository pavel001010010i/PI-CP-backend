using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AviaTM.Services.IServicesController;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Infastructure;
using Constant;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeCargoController : ControllerBase
    {
        private readonly ITypeCargoControllerService _context;
        private readonly UserContext _userContext;
        public TypeCargoController(ITypeCargoControllerService context, UserContext userContext) {
            _context = context;
            _userContext = userContext;
        }

        [HttpGet("get")]
        
        public async Task<IActionResult> GetTypeCargo()
        {
            

            var typeCargo = _context.GetTypeCargos();
            if (typeCargo == null)
            {
                return BadRequest();
            }
            return Ok(typeCargo);
        }

        [HttpGet("get-type-cargo")]
        public async Task<IActionResult> Get()
        {
            var typeCarg = _context.GetTypeCargosForAdmin();
            if (typeCarg == null)
            {
                return BadRequest();
            }
            return Ok(typeCarg);
        }

        [HttpPut("update")]
        [Authorize(Roles = AspNetRoles.Admin)]
        public async Task<IActionResult> UpdateTypeCargo([FromBody] TypeCargo model)
        {
            await _context.UpdateTypeCargos(model);
            return Ok();
        }
    }
}
