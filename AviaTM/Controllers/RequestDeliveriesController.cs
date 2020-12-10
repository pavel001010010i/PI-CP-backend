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
using AviaTM.methods;
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestDeliveriesController : ControllerBase
    {
        private readonly IRDRepository _repository;
        public RequestDeliveriesController(IRDRepository context) => _repository = context;

        [HttpPost("/getDis")]
        public async Task<ActionResult<object>> GetDistance([FromBody]Coordinates coordinates)
        {
            GetDistance getDistance = new GetDistance();

            return getDistance.DistanceEarth(coordinates.X1, coordinates.Y1, coordinates.X2, coordinates.Y2);
        }


        [Authorize]
        [HttpPost("/addrd")]
        public async Task<ActionResult<object>> PostRDAdd(RDBody rd)
        {
            if (User.IsInRole("admin"))
            {
                var response = new
                {
                    succes = false,
                    message = "Administrator can't add requests"
                };
                return response;
            }
            var customerId = await _repository.GetCustomerId(rd.CustomerEmail);
            var cargoId = await _repository.GetCargoId(rd.CargoName);
            var isCargoExistForRD = await _repository.GetRD(cargoId);
            if (isCargoExistForRD!=null)
            {
                var response = new
                {
                    succes = false,
                    message = "!The request for delivery of this product already exists!"
                };
                return response;
            }
            var isCargoExistOrder = await _repository.GetOrder(cargoId);

            if (isCargoExistOrder != null)
            {
                var response = new
                {
                    succes = false,
                    message = "!This product has already been accepted by the supplier company!"
                };
                return response;
            }
            else
            {
                RequestDelivery request = new RequestDelivery()
                {
                    ProviderId = rd.ProviderId,
                    CustomerId = customerId,
                    CargoId = cargoId,
                    CountryIdFrom = rd.CountryNameFrom,
                    CountryIdTo = rd.CountryNameTo,
                    DateDeparture = rd.dateDep,
                    DateDelivery = rd.dateDel,
                    CastDelivery = rd.castDep,
                    StatusRequest = true
                };

                await _repository.AddRD(request);
                var response = new
                {
                    succes = true,
                    message = "Application form completed"
                };
                return response;

            }

         
        }
        // GET: api/RequestDeliveries
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<List<RequestDelivery>>> GetRequestDeliveries()
        {
            if (User.IsInRole("admin"))
            {
                return await _repository.GetRDAdmin();
            }
            if (User.IsInRole("provider"))
            {
                return await _repository.GetRDProvider(User);

            }
            return await _repository.GetRDCustomer(User);
        }

        // GET: api/RequestDeliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestDelivery>> GetRequestDelivery(int id)
        {
            var requestDelivery = await _repository.GetRDOriginal(id);

            if (requestDelivery == null)
            {
                return NotFound();
            }
            return requestDelivery;
        }

        // PUT: api/RequestDeliveries/5
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutRequestDelivery(int id, RequestDelivery requestDelivery)
        {
            var requestDeliveryEx = await _repository.GetRDOriginal(id);
            if (requestDeliveryEx== null)
            {
                return BadRequest();
            }

            return await _repository.UpdateRD(requestDelivery);
        }

        // POST: api/RequestDeliveries
        [HttpPost]
        public async Task<ActionResult<object>> PostRequestDelivery(RequestDelivery requestDelivery)
        {
            await _repository.AddRD(requestDelivery);

            var response = new
            {
                succes = true,
                message = "Application form completed"
            };
            return response;
        }

        // DELETE: api/RequestDeliveries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteRequestDelivery(int id)
        {
            var requestDelivery = await _repository.GetRDOriginal(id);
            if (requestDelivery == null)
            {
                return NotFound();
            }

            return await _repository.RemoveRD(requestDelivery);
        }
    }
}
