using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ITypeCargoControllerService
    {
        IEnumerable<TypeCargo> GetTypeCargos();
        IEnumerable<TypeCargo> GetTypeCargosForAdmin();
        Task UpdateTypeCargos(TypeCargo model);
    }
}
