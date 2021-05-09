using System.Collections.Generic;
using System.Threading.Tasks;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;

namespace AviaTM.Services.IServicesController
{
    public interface IUserControllerService
    {
        Task<IEnumerable<AppUser>> GetAllUsersByUserRole();
        Task<ResponseMessageModel> UpdateUserLockout(string id);
    }
}
