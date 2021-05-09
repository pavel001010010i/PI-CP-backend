using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.DB.RepositoryService.IRepositoryService
{
    public interface ITypeCargoService
    {
        Task<IEnumerable<TypeCargo>> GetTypeCagroes();
    }
}
