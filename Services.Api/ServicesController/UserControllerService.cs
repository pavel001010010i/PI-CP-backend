using AviaTM;
using AviaTM.DB.Model.Models;
using AviaTM.Services.IServicesController;
using AviaTM.Services.Models.Models;
using Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace AviaTm.Services.Api.ServicesController
{
    public class UserControllerService : IUserControllerService
    {
        public ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IOptions<AuthorizationSettings> _authSettings;
        public UserControllerService(ApplicationDbContext context, 
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IOptions<AuthorizationSettings> authSettings)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _authSettings = authSettings;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersByUserRole()
        {
            return await _userManager.GetUsersInRoleAsync(AspNetRoles.User);
        }

        public async Task<ResponseMessageModel> UpdateUserLockout(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                user.isLockdown = !user.isLockdown;
                await _userManager.UpdateAsync(user);

                return new ResponseMessageModel
                {
                    Status = true,
                    Message = string.Format("Статус блокировки пользователя {0} обновлен", user.UserName)
                };
            }
            return new ResponseMessageModel
            {
                Status = false,
                Message = "Пользователь не найден!"
            };
        }
    }
}
