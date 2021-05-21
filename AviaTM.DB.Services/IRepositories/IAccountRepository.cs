using System.Collections.Generic;
using System.Threading.Tasks;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;

namespace AviaTM.DB.IRepository
{
    public interface IAccountRepository
    {
        Task<AppUser> FindUser(string login);
        Task<AppUser> FindUserById(string id);
        Task<bool> IsPasswordValid(AppUser user, string password);
        Task<string> GenerateToken(AppUser checkedUser);
        Task<ResponseMessageModel> RegistrationUser(RegisterUserModel model);
    }
}
