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

namespace AviaTM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlanesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin,provider")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plane>>> GetPlane()
        {
            if (User.IsInRole("admin"))
            {
                var plane = _context.Plane.Include(p => p.Provider);
                return await plane.ToListAsync();
            }
            else
            {
                var plane = _context.Plane.Include(p => p.Provider).Where(x => x.Provider.Email == User.Identity.Name);
                return await plane.ToListAsync();
            }
        }

        [Authorize(Roles = "admin,provider")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Plane>> GetPlane(int id)
        {
            var plane = await _context.Plane.FindAsync(id);

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
            if (id != plane.Id)
            {
                return BadRequest();
            }

            _context.Entry(plane).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var response = new
                {
                    succes = true,
                    message = "User update!"
                };
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        [Authorize(Roles = "admin,provider")]
        [HttpPost]
        public async Task<ActionResult<object>> PostPlane(PlaneBody planeBody)
        {
            Provider provider = _context.Provider.Where(x => x.NameCompany == planeBody.NameCompany).FirstOrDefault();
            Provider newProv = provider;
            provider = null;
            Plane plane = new Plane()
            {
                IdProvider = newProv.ProviderId,
                NamePlane = planeBody.NamePlane,
                ModelPlane = planeBody.ModelPlane,
                Height = planeBody.Height,
                Width = planeBody.Width,
                depth = planeBody.depth,
                CapacityWeight = planeBody.CapacityWeight
            };
            _context.Plane.Add(plane);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Plane add"
            };
            
            return response;
        }

        [Authorize(Roles = "admin,provider")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlane(int id)
        {
            var plane = await _context.Plane.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }

            _context.Plane.Remove(plane);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaneExists(int id)
        {
            return _context.Plane.Any(e => e.Id == id);
        }
    }
}
