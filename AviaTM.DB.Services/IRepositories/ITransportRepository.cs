using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ITransportRepository
    {
        IEnumerable<Transport> GetTransports();
        Task AddTransport(RouteModel model);
        Task<ResponseMessageModel> DeleteTransport(int id);
        Task UpdateTransport(RouteModel model);
        Task<IEnumerable<Transport>> SearchTransport(SearchtModel model);
    }
}
