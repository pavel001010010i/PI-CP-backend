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
    public class TypeTransportControllerService : ITypeTransportControllerService
    {
        private readonly ApplicationDbContext _context;

        public TypeTransportControllerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TypeTransport> GetTypeTransportId(int id)
        {
            return await _context.TypeTransports.FindAsync(id);
        }

        public async Task<IEnumerable<TypeTransport>> GetTypeTransports()
        {
           return _context.TypeTransports.Where(x=>x.isActive).OrderBy(x => x.Id).ToList();
        }

        public async Task<IEnumerable<TypeTransport>> GetTypeTransportsForAdmin()
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
