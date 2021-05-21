using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ITransportLoadCapacityRepository
    {
        IEnumerable<TransportLoadCapacity> GetTransportLoadCapacies();
        Task<TransportLoadCapacity> GetTransportLoadCapacityId(int id);
    }
}
