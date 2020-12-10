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
    public class PlanesController : ControllerBase
    {
        private readonly IPlaneRepository _repository;
        public PlanesController(IPlaneRepository context) => _repository = context;

        [Authorize(Roles = "admin,provider")]
        [HttpGet]
        public async Task<ActionResult<List<Plane>>> GetPlane()
        {
            if (User.IsInRole("admin"))
            {
                return await _repository.GetPlanesAdmin();
            }
            else
            {
                return await _repository.GetPlanesUser(User);
            }
        }

        [Authorize(Roles = "admin,provider")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Plane>> GetPlane(int id)
        {
            var plane = await _repository.GetPlane(id);
            if (plane == null)
            {
                return NotFound();
            }

            return plane;
        }

        [Authorize(Roles = "admin,provider")]
        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutPlane(int id, Plane plane)
        {
            var planeEx = await _repository.GetPlane(id);
            if (planeEx == null)
            {
                return BadRequest();
            }
            return await _repository.UpdatePlane(plane);
        }

        [Authorize(Roles = "admin,provider")]
        [HttpPost]
        public async Task<ActionResult<object>> PostPlane(PlaneBody planeBody)
        {
            Provider provider = await _repository.GetProvider(planeBody);
            Plane plane = new Plane()
            {
                IdProvider = provider.ProviderId,
                NamePlane = planeBody.NamePlane,
                ModelPlane = planeBody.ModelPlane,
                Height = planeBody.Height,
                Width = planeBody.Width,
                depth = planeBody.depth,
                CapacityWeight = planeBody.CapacityWeight
            };
            return await _repository.AddPlane(plane);
        }

        [Authorize(Roles = "admin,provider")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeletePlane(int id)
        {
            var plane = await _repository.GetPlane(id);
            if (plane == null)
            {
                return NotFound();
            }

            return await _repository.RemovePlane(plane);
        }
    }
}
