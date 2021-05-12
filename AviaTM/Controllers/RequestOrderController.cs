using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AviaTM.Services.IServicesController;
using AviaTM.DB.Model.Models;
using Constant;
using AviaTM.Services.Models.Models;
using AviaTM.Services.Models.Infastructure;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestOrderController : ControllerBase
    {
        private readonly IRecOrdControllerService _context;
        private readonly UserContext _userContext;
        public RequestOrderController(IRecOrdControllerService context, UserContext userContext) 
        { 
            _context = context;
            _userContext = userContext;
        }

        [HttpGet("get-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetId(int id)
        {
            var requestData = await _context.GetRequestMainId(id);

            if (requestData == null)
            {
                return BadRequest();
            }

            return Ok(requestData);
        }

        [HttpPost("add-item")]
        [Authorize]
        public async Task<IActionResult>AddRequestItem([FromBody] RequestModel model)
        {
            var message = await _context.Add(model);
            if (!message.Status)
            {
                return BadRequest(message);
            }
            return Ok(message);
        }

        [HttpGet("get-requests")]
        public IActionResult Get()
        {
            var requests = _context.GetRequests();
            if (requests == null)
            {
                return BadRequest();
            }
            return Ok(requests);
        }

        [HttpPut("update")]
        [Authorize(Roles = AspNetRoles.User)]
        public async Task<IActionResult> UpdateTypeCargo([FromBody] RequestModel model)
        {
            await _context.Update(model);
            return Ok();
        }

        [HttpGet("get-requests-providers")]
        [Authorize]
        public IActionResult GetRequestProvoders()
        {
            var requests =_context.GetRequestsUserProvider(_userContext.UserId);
            return Ok(requests);
        }

        [HttpGet("get-requests-customers")]
        [Authorize]
        public IActionResult GetRequestCustomers()
        {
            var requests = _context.GetRequestsUserCustomer(_userContext.UserId);
            return Ok(requests);
        }

        [HttpPost("delete-item")]
        [Authorize]
        public async Task<IActionResult> DeleteRequestCustomer([FromBody] RequestMain model)
        {
            var response = await _context.DeleteRequestCustomer(model);
            return Ok(response);
        }



    }
}
