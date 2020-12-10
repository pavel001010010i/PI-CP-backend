using AviaTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AviaTM.Interfaces
{
    public interface IPlaneRepository
    {
        Task <List<Plane>> GetPlanesAdmin();
        Task<List<Plane>> GetPlanesUser(ClaimsPrincipal User);
        Task<Plane> GetPlane(int id);
        Task<object> UpdatePlane(Plane plane);
        Task<Provider> GetProvider(PlaneBody planeBody);
        Task<object> AddPlane(Plane plane);
        Task<object> RemovePlane(Plane plane);
    }
}
