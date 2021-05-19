using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ITypePaymentRepository
    {
        IEnumerable<TypePayment> GetTypePayments();
        Task<TypePayment> GetTypePaymentId(int id);
        IEnumerable<TypePayment> GetTypeForAdmin();
        Task Update(TypePayment model);
    }
}
