using AviaTM;
using AviaTM.DB.IRepository;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using Constant;
using Microsoft.AspNetCore.Identity;
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


namespace AviaTm.DB.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<AuthorizationSettings> _authSettings;
        public AccountRepository(ApplicationDbContext context, 
            UserManager<AppUser> userManager, 
            IOptions<AuthorizationSettings> authSettings)
        {
            _context = context;
            _userManager = userManager;
            _authSettings = authSettings;
        }
        public async Task<ResponseMessageModel> RegistrationUser(RegisterUserModel model)
        {
            var userName = _userManager.FindByNameAsync(model.UserName).Result;
            var userEmail = _userManager.FindByEmailAsync(model.Email).Result;

            if (userName == null && userEmail == null)
            {
                AppUser user = new AppUser
                {
                    Name = model.Name,
                    NameOrganization = model.NameOrganization,
                    UNP = model.UNP,
                    AnotherContact = model.AnotherContact,
                    Address = model.Address,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, AspNetRoles.User);
                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Регистрация прошла успешно!"
                };
            } 
            else if (userName != null)
            {
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Пользователь с данным ЛОГИНОМ существует!"
                };
            }
            else if (userEmail != null)
            {
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Пользователь с данным ЕМАЙЛОМ существует!"
                };
            }
            return new ResponseMessageModel
            {
                Status = true,
                Message = "Prosto vivod"
            };

        }

        public async Task<AppUser> FindUser(string login) 
        {
            return await _userManager.FindByNameAsync(login);
        }

        public async Task<AppUser> FindUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<bool> IsPasswordValid(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GenerateToken(AppUser user)
        {
            var now = DateTime.UtcNow;

            var authSettings = _authSettings.Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.SecretKey));

            var options = new TokenProviderOptions
            {
                Audience = authSettings.Audience,
                Issuer = authSettings.Issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromDays(authSettings.ExpirationDays)
            };

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            claims.AddRange(
                (await _userManager.GetRolesAsync(user)).Select(r => new Claim("roles", r)));

            var jwt = new JwtSecurityToken(
                issuer: authSettings.Issuer,
                audience: authSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            }; ;

            return JsonConvert.SerializeObject(encodedJwt, serializerSettings);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
