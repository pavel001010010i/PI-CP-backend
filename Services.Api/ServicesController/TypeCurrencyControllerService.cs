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
    public class TypeCurrencyControllerService : ITypeCurrencyControllerService
    {
        private readonly ApplicationDbContext _context;

        public TypeCurrencyControllerService(ApplicationDbContext context)
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
