using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ITypePaymentControllerService
    {
        IEnumerable<TypePayment> GetTypePayments();
        Task<TypePayment> GetTypePaymentId(int id);
        IEnumerable<TypePayment> GetTypeForAdmin();
        Task Update(TypePayment model);
    }
}
