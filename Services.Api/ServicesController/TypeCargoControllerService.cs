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
    public class TypeCargoControllerService : ITypeCargoControllerService
    {
        private readonly ApplicationDbContext _context;

        public TypeCargoControllerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<TypeCargo> GetTypeCargos()
        {
           return _context.TypeCargoes.Where(x => x.isActive).OrderBy(x => x.Id).ToList();
        }
        public IEnumerable<TypeCargo> GetTypeCargosForAdmin()
        {
            return _context.TypeCargoes.OrderBy(x => x.Id).ToList();
        }
        public async Task UpdateTypeCargos(TypeCargo model)
        {
            _context.Attach(model);
            _context.TypeCargoes.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
