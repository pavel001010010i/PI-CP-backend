using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ITypeTransportRepository
    {
        IEnumerable<TypeTransport> GetTypeTransports();
        IEnumerable<TypeTransport> GetTypeTransportsForAdmin();
        Task Update(TypeTransport model);
        Task<TypeTransport> GetTypeTransportId(int id);
        
    }
}
