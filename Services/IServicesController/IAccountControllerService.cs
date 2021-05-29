using System.Collections.Generic;
using System.Threading.Tasks;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;

namespace AviaTM.Services.IServicesController
{
    public interface IAccountControllerService
    {
        Task<AppUser> FindUser(string login);
        Task<AppUser> FindUserById(string id);
        Task<bool> IsPasswordValid(AppUser user, string password);
        Task<string> GenerateToken(AppUser checkedUser);
        Task<ResponseMessageModel> RegistrationUser(RegisterUserModel model);
        Task SendEmailAsync(string email, string subject, string message);
        Task<bool> UserIsConfirmed(AppUser user);
        Task<ResponseMessageModel> ConfirmEmail(ResponseConfirmEmailModel model);
    }
}
