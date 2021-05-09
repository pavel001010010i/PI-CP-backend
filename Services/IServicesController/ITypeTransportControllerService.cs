using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ITypeTransportControllerService
    {
        Task<IEnumerable<TypeTransport>> GetTypeTransports();
        Task<IEnumerable<TypeTransport>> GetTypeTransportsForAdmin();
        Task Update(TypeTransport model);
        Task<TypeTransport> GetTypeTransportId(int id);
        
    }
}
