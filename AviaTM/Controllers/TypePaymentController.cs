using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AviaTM.Services.IServicesController;
using AviaTM.DB.Model.Models;
using Constant;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypePaymentController : ControllerBase
    {
        private readonly ITypePaymentControllerService _context;
        public TypePaymentController(ITypePaymentControllerService context) => _context = context;

        [HttpGet("get")]
        //[Authorize]
        public async Task<IActionResult> GetTypePayment()
        {
            var type = _context.GetTypePayments();
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }

        [HttpGet("get-item/{id}")]
        public async Task<IActionResult> GetTypePayment(int id)
        {
            var type = await _context.GetTypePaymentId(id);
            if (type == null)
            {
                return BadRequest();
            }
            return Ok(type);
        }

        [HttpGet("get-type-payment")]
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
        public async Task<IActionResult> UpdateTypeCargo([FromBody] TypePayment model)
        {
            await _context.Update(model);
            return Ok();
        }

    }
}
