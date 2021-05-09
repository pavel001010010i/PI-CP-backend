using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ITransportLoadCapacityControllerService
    {
        Task<IEnumerable<TransportLoadCapacity>> GetTransportLoadCapacies();
        Task<TransportLoadCapacity> GetTransportLoadCapacityId(int id);
    }
}
