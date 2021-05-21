using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface IRouteMapRepository
    {
        Task<IEnumerable<RouteMap>> GetRouteMaps();
        Task<RouteMap> GetRouteMap(int id);
        Task UpdateRouteMap(RouteModel model);
    }
}
