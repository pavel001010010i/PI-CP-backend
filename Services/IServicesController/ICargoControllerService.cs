using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ICargoControllerService
    {
        Task<IEnumerable<Cargo>> GetCargos();
        Task AddCargos(RouteModel model);
        Task DeleteCargo(int id);
        Task UpdateCargo(RouteModel model);
        Task<IEnumerable<Cargo>> SearchCargo(SearchtModel model);
    }
}
