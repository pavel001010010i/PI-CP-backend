using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ITypeCurrencyRepository
    {
        IEnumerable<TypeCurrency> GetTypeCurrencyies();
        Task <TypeCurrency> GetTypeCurrencyId(int id);
        IEnumerable<TypeCurrency> GetTypeForAdmin();
        Task  Update(TypeCurrency model);
    }
}
