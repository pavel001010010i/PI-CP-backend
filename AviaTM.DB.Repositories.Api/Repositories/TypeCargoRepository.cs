using AviaTM;
using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTM.DB.IRepository;

namespace AviaTm.DB.Repository
{
    public class TypeCargoRepository : ITypeCargoRepository
    {
        private readonly ApplicationDbContext _context;

        public TypeCargoRepository(ApplicationDbContext context)
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
