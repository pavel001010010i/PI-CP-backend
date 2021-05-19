using System.Collections.Generic;
using System.Threading.Tasks;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;

namespace AviaTM.DB.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersByUserRole();
        Task<ResponseMessageModel> UpdateUserLockout(string id);
    }
}
