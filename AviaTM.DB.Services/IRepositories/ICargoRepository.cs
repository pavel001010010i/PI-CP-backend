using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ICargoRepository
    {
        IEnumerable<Cargo> GetCargos();
        IEnumerable<Cargo> GetCargoesForSelectRequest();
        Task<Cargo> GetCargoId(int id);
        Task AddCargos(RouteModel model);
        Task DeleteCargo(int id);
        Task UpdateCargo(RouteModel model);
        Task<IEnumerable<Cargo>> SearchCargo(SearchtModel model);
    }
}
