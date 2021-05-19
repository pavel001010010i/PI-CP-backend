using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.DB.IRepository
{
    public interface ITypeCargoRepository
    {
        IEnumerable<TypeCargo> GetTypeCargos();
        IEnumerable<TypeCargo> GetTypeCargosForAdmin();
        Task UpdateTypeCargos(TypeCargo model);
    }
}
