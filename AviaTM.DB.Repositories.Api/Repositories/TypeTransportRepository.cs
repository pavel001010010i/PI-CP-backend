using AviaTM;
using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTM.DB.IRepository;

namespace AviaTm.DB.Repository
{ 
    public class TypeTransportRepository : ITypeTransportRepository
    {
        private readonly ApplicationDbContext _context;

        public TypeTransportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TypeTransport> GetTypeTransportId(int id)
        {
            return await _context.TypeTransports.FindAsync(id);
        }

        public IEnumerable<TypeTransport> GetTypeTransports()
        {
           return _context.TypeTransports.Where(x=>x.isActive).OrderBy(x => x.Id).ToList();
        }

        public IEnumerable<TypeTransport> GetTypeTransportsForAdmin()
        {
            return _context.TypeTransports.OrderBy(x => x.Id).ToList();
        }

        public async Task Update(TypeTransport model)
        {
            _context.Attach(model);
            _context.TypeTransports.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
