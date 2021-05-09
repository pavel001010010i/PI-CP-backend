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
    public class TypePaymentControllerService : ITypePaymentControllerService
    {
        private readonly ApplicationDbContext _context;

        public TypePaymentControllerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TypePayment> GetTypePaymentId(int id)
        {
            return await _context.TypePayment.FindAsync(id);
        }

        public IEnumerable<TypePayment> GetTypePayments()
        {
           return _context.TypePayment.Where(x=>x.isActive).ToList();
        }
        public IEnumerable<TypePayment> GetTypeForAdmin()
        {
            return _context.TypePayment.OrderBy(x => x.Id).ToList();
        }

        public async Task Update(TypePayment model)
        {
            _context.Attach(model);
            _context.TypePayment.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
