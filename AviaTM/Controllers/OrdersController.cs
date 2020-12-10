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
using AviaTM.Interfaces;

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        public OrdersController(IOrderRepository context) => _repository = context;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrder()
        {
            if (User.IsInRole("admin"))
            {
                return await _repository.GetOrderAdmin();
            }
            if (User.IsInRole("provider"))
            {
                return await _repository.GetOrderProvider(User);

            }
            return await _repository.GetOrderCustomer(User);
        }

        [HttpPost("/addorder")]
        public async Task<ActionResult<object>> PostOrderAddFromRD(RequestDelivery delivery)
        {
            try
            {
                var kek = delivery;
                var plane = await _repository.GetPlane(delivery);
                Random rand = new Random(DateTime.Now.ToString().GetHashCode());
                int index = rand.Next(0, plane.Count);
                Order order = new Order()
                {
                    CustomerId = delivery.CustomerId,
                    ProviderId = delivery.ProviderId,
                    CargoId = delivery.CargoId,
                    CountryIdTo = delivery.CountryIdTo,
                    CountryIdFrom = delivery.CountryIdFrom,
                    PlaneId = plane[index].Id,
                    DateDeparture = delivery.DateDeparture,
                    DateDelivery = delivery.DateDelivery,
                    Status = "confirmation",
                    CastDelivery = delivery.CastDelivery
                };
                await _repository.AddOder(order);

                var del = await _repository.GetRD(delivery.IdRequest);
                await _repository.RemoveRD(del);
                var response = new
                {
                    succes = true,
                    message = "Order form completed"
                };
                return response;
            }
            catch
            {
                var response = new
                {
                    succes = false,
                    message = "Oops :("
                };
                return response;
            }
           
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteOrder(int id)
        {
            var cust = await _repository.GetCustomer(User);
            if (cust != null)
            {
                var customer = await _repository.GetOrderWhere(cust.Id);
                if (customer != null)
                {
                    var order = await _repository.GetOrder(id);
                    await _repository.RemoveOrder(order);
                    var respons = new
                    {
                        succes = true,
                        message = "Order deleted!"
                    };
                    return respons;
                }
            }
            var prov = await _repository.GetProvider(User);
            if (prov != null)
            {
                var provider = await _repository.GetOrderWhereP(prov.ProviderId);
                if (provider != null)
                {
                    var order = await _repository.GetOrder(id);
                    await _repository.RemoveOrder(order);
                    var respons = new
                    {
                        succes = true,
                        message = "Order deleted!"
                    };
                    return respons;
                }
            }
            if (User.IsInRole("admin")){
                var order = await _repository.GetOrder(id);
                await _repository.RemoveOrder(order);
                var respons = new
                {
                    succes = true,
                    message = "Order deleted!"
                };
                return respons;
            }
            var response = new
            {
                succes = false,
                message = "You can't delete it :("
            };
            return response;
        }
    }
}
