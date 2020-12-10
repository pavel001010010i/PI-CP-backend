using AviaTM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AviaTM.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public ApplicationDbContext _context;

        public async Task<User> GetUserP(ProviderBody providerBody)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Login == providerBody.Email);
        }

        public async Task<RequestUser> GetRequestUserP(ProviderBody providerBody)
        {
            return await _context.RequestUser.FirstOrDefaultAsync(x => x.Login == providerBody.Email);
        }

        public async Task<Customer> GetCustomersP(ProviderBody providerBody)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == providerBody.Email);
        }

        public async Task<Provider> GetProviderP(ProviderBody providerBody)
        {
            return await _context.Provider.FirstOrDefaultAsync(x => x.Email == providerBody.Email);
        }
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddRequestUser(RequestUser requestUser)
        {
            await _context.RequestUser.AddAsync(requestUser);
            await _context.SaveChangesAsync();
        }
        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddProvider(Provider provider)
        {
            await _context.Provider.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomers(CustomerBody userBody)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == userBody.Email);
        }

        public async Task<Provider> GetProvider(CustomerBody userBody)
        {
            return await _context.Provider.FirstOrDefaultAsync(x => x.Email == userBody.Email);
           
        }

        public async Task<RequestUser> GetRequestUser(CustomerBody userBody)
        {
            return await _context.RequestUser.FirstOrDefaultAsync(x => x.Login == userBody.Email);
        }
        public async Task<User> GetUser(CustomerBody userBody)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Login == userBody.Email);
        }

        public object GetToken(UserBody user)
        {
            var identity = GetIdentity(user.Login, user.Password);
            if (identity == null)
            {
                var ur = _context.RequestUser.FirstOrDefault(x => x.Login == user.Login);
                if (ur != null)
                {
                    var respons = new
                    {
                        succes = false,
                        message = "Your application has not been confirmed yet :("
                    };
                    return respons;
                }
            }

            var user1 = _context.User.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password);
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = user1.Role,
                lockoutEnable = user1.LockoutEnable
            };

            return response;
        }


        private ClaimsIdentity GetIdentity(string login, string password)
        {

            var user = _context.User.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        
    }
}
