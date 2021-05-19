using AviaTM;
using AviaTM.DB.IRepository;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using Constant;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AviaTm.DB.Repository
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(ApplicationDbContext context, 
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
