using AviaTM;
using AviaTM.DB.Model.Models;
using AviaTM.DB.Model;
using AviaTM.Services.IServicesController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTm.Services.Api.ServicesController
{
    public class TransportLoadCapacityControllerService : ITransportLoadCapacityControllerService
    {
        private readonly ApplicationDbContext _context;

        public TransportLoadCapacityControllerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransportLoadCapacity>> GetTransportLoadCapacies()
        {
            return _context.TransportLoadCapacity.ToList();
        }

        public async Task<TransportLoadCapacity> GetTransportLoadCapacityId(int id)
        {
            return await _context.TransportLoadCapacity.FindAsync(id);
        }
    }
}
