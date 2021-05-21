using AviaTM;
using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTM.DB.IRepository;

namespace AviaTm.DB.Repository
{
    public class TransportLoadCapacityRepository : ITransportLoadCapacityRepository
    {
        private readonly ApplicationDbContext _context;

        public TransportLoadCapacityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TransportLoadCapacity> GetTransportLoadCapacies()
        {
            return _context.TransportLoadCapacity.ToList();
        }

        public async Task<TransportLoadCapacity> GetTransportLoadCapacityId(int id)
        {
            return await _context.TransportLoadCapacity.FindAsync(id);
        }
    }
}
