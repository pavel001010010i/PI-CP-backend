using AviaTM;
using AviaTM.DB.Model.Models;
using AviaTM.Services.IServicesController;
using AviaTM.Services.Models.Models;
using Constant;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
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
    public class AccountControllerService : IAccountControllerService
    {
        public ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOptions<AuthorizationSettings> _authSettings;
        private static string userId = "";
        public AccountControllerService(ApplicationDbContext context, 
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

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, AspNetRoles.User);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    await SendEmailAsync(model.Email, "Подтверждение регистрации вашего аккаунта",
                        $"Для подтверждения пароля используйте код: {code}");
                }
                userId = user.Id;
                return new ResponseMessageModel
                {
                    Status = result.Succeeded,
                    Message = "Для завершения регистрации проверьте электронную почту и ведите код, указанный в письме!",
                };
            } 
            else if (userName != null)
            {
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Пользователь с данным логином существует!"
                };
            }
            else if (userEmail != null)
            {
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Пользователь с данной Эл. почтой существует!"
                };
            }
            return new ResponseMessageModel
            {
                Status = true,
                Message = "Prosto vivod"
            };

        }
        public async Task<ResponseMessageModel> ConfirmEmail(ResponseConfirmEmailModel model)
        {
            if (userId == null || model.Code == null)
            {
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Код пуст!"

                };
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Пользователь не найден"

                };
            }
            var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            if (result.Succeeded)
                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Вы подтвердили электронную почту!"

                };
            else
                return new ResponseMessageModel
                {
                    Status = false,
                    Message = "Код не верный!"

                };
        }
        public async Task<bool> UserIsConfirmed(AppUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
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

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("ITransport", "varetspavel@yandex.by"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("varetspavel@yandex.by", "sioihezaodpghbts");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }

}
