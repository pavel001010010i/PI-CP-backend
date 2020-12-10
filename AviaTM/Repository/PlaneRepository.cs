using AviaTM.Interfaces;
using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AviaTM.Repository
{
    public class PlaneRepository: IPlaneRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> AddPlane(Plane plane)
        {
            _context.Plane.Add(plane);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Plane added!"
            };
            return response;
        }

        public async Task<Plane> GetPlane(int id)=> await _context.Plane.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<Plane>> GetPlanesAdmin()
        {
            return await _context.Plane.Include(p => p.Provider).ToListAsync();
        }

        public async Task<List<Plane>> GetPlanesUser(ClaimsPrincipal User)
        {
            return await _context.Plane.Include(p => p.Provider).Where(x => x.Provider.Email == User.Identity.Name).ToListAsync();
        }

        public async Task<Provider> GetProvider(PlaneBody planeBody) => await _context.Provider.AsNoTracking()
                .SingleOrDefaultAsync(x => x.NameCompany == planeBody.NameCompany);

        public async Task<object> RemovePlane(Plane plane)
        {
            _context.Plane.Remove(plane);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Plane deleted!"
            };
            return response;
        }

        public async Task<object> UpdatePlane(Plane plane)
        {
            _context.Plane.Update(plane);
            await _context.SaveChangesAsync();
            var response = new
            {
                succes = true,
                message = "Plane updated!"
            };
            return response;
        }
    }
}
