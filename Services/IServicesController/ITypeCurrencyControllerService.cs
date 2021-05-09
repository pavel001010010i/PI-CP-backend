using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ITypeCurrencyControllerService
    {
        IEnumerable<TypeCurrency> GetTypeCurrencyies();
        Task <TypeCurrency> GetTypeCurrencyId(int id);
        IEnumerable<TypeCurrency> GetTypeForAdmin();
        Task  Update(TypeCurrency model);
    }
}
