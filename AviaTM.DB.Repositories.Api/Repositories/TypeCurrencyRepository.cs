using AviaTM;
using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTM.DB.IRepository;

namespace AviaTm.DB.Repository
{
    public class TypeCurrencyRepository : ITypeCurrencyRepository
    {
        private readonly ApplicationDbContext _context;

        public TypeCurrencyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TypeCurrency> GetTypeCurrencyId(int id)
        {
            return await _context.TypeCurrency.FindAsync(id);
        }

        public IEnumerable<TypeCurrency> GetTypeCurrencyies()
        {
           return _context.TypeCurrency.Where(x=>x.isActive).OrderBy(x => x.Id).ToList();
        }
        public IEnumerable<TypeCurrency> GetTypeForAdmin()
        {
            return _context.TypeCurrency.OrderBy(x => x.Id).ToList();
        }

        public async Task Update(TypeCurrency model)
        {
            _context.Attach(model);
            _context.TypeCurrency.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
