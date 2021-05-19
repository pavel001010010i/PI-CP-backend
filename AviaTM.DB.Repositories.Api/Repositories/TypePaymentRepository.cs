using AviaTM;
using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTM.DB.IRepository;

namespace AviaTm.DB.Repository
{
    public class TypePaymentRepository : ITypePaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public TypePaymentRepository(ApplicationDbContext context)
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
