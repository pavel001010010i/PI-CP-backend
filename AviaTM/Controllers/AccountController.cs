using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AviaTM.Models; // класс Person
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AviaTM.Controllers
{

    public class UserBody
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
    public class AccountController : ControllerBase
    {
        public ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        // тестовые данные вместо использования базы данных

        [HttpPost("/token")]
        public async Task<ActionResult<object>> Token([FromBody] UserBody user)
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

        [HttpPost("/addcustomer")]
        public async Task<ActionResult<object>> AddCustomer([FromBody] CustomerBody userBody)
        {
            var userT = await _context.User.FirstOrDefaultAsync(x => x.Login == userBody.Email);
            var userReques = await _context.RequestUser.FirstOrDefaultAsync(x => x.Login == userBody.Email);
            var userCust = await _context.Customers.FirstOrDefaultAsync(x => x.Email == userBody.Email);
            var userProv = await _context.Provider.FirstOrDefaultAsync(x => x.Email == userBody.Email);
            if (!ModelState.IsValid)
            {
                var response = new
                {
                    exist = false,
                    message = "Model not is valid"
                };
                return response;
            }
            if (User.IsInRole("admin"))
            {
                if (userT == null && userCust == null && userProv == null && userReques==null)
                {
                    User user = new User() { Login = userBody.Email, Password = userBody.Password, Role = "customer", LockoutEnable = true };
                    Customer customer = new Customer()
                    {
                        FirstName = userBody.FirstName,
                        LastName = userBody.LastName,
                        Email = userBody.Email,
                        Age = userBody.Age,
                        PassportData = userBody.PassportData,
                        Sex = userBody.Sex
                    };

                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    await _context.User.AddAsync(user);
                    await _context.SaveChangesAsync();
                    var response = new
                    {
                        exist = true,
                        message = "User added successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }
            }
            else
            {
                if (userT == null && userCust == null && userProv == null && userReques == null)
                {
                    RequestUser ru = new RequestUser { Login = userBody.Email, Password = userBody.Password, Role = "customer"};
                    Customer customer = new Customer()
                    {
                        FirstName = userBody.FirstName,
                        LastName = userBody.LastName,
                        Email = userBody.Email,
                        Age = userBody.Age,
                        PassportData = userBody.PassportData,
                        Sex = userBody.Sex
                    };

                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    await _context.RequestUser.AddAsync(ru);
                    await _context.SaveChangesAsync();
                    var response = new
                    {
                        exist = true,
                        message = "Your request has been sent. Wait for admin approval"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }

            }
            
        }
        [HttpPost("/addprovider")]
        public async Task<ActionResult<object>> AddProvider([FromBody] ProviderBody providerBody)
        {
            var userT = await _context.User.FirstOrDefaultAsync(x => x.Login==providerBody.Email);
            var userReques = await _context.RequestUser.FirstOrDefaultAsync(x => x.Login == providerBody.Email);
            var userCust = await _context.Customers.FirstOrDefaultAsync(x => x.Email == providerBody.Email);
            var userProv = await _context.Provider.FirstOrDefaultAsync(x => x.Email == providerBody.Email);
            if (!ModelState.IsValid)
            {
                var response = new
                {
                    exist = false,
                    message = "Model not is valid"
                };
                return response;
            }
            if (User.IsInRole("admin"))
            {
                if (userT == null && userCust == null && userProv == null && userReques==null)
                {
                    User user = new User() { Login = providerBody.Email, Password = providerBody.Password, Role = "provider", LockoutEnable = true };
                    Provider provider = new Provider()
                    {
                        NameCompany = providerBody.NameCompany,
                        LicenceNumber = providerBody.LicenceNumber,
                        Email = providerBody.Email,
                        PhoneNumber = providerBody.PhoneNumber,
                        CountresProvider = providerBody.CountresProvider
                    };

                    await _context.Provider.AddAsync(provider);
                    await _context.SaveChangesAsync();
                    await _context.User.AddAsync(user);
                    await _context.SaveChangesAsync();
                    var response = new
                    {
                        exist = true,
                        message = "User added successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }
            }
            else
            {
                if (userT == null && userCust == null && userProv == null && userReques == null)
                {
                    RequestUser ru = new RequestUser { Login = providerBody.Email, Password = providerBody.Password, Role = "provider" };
                    Provider provider = new Provider()
                    {
                        NameCompany = providerBody.NameCompany,
                        LicenceNumber = providerBody.LicenceNumber,
                        Email = providerBody.Email,
                        PhoneNumber = providerBody.PhoneNumber,
                        CountresProvider = providerBody.CountresProvider
                    };

                    await _context.Provider.AddAsync(provider);
                    await _context.SaveChangesAsync();
                    await _context.RequestUser.AddAsync(ru);
                    await _context.SaveChangesAsync();
                    var response = new
                    {
                        exist = true,
                        message = "User added successfully"
                    };
                    return response;
                }
                else
                {
                    var response = new
                    {
                        exist = false,
                        message = "The application with the given usernames or the user already exists"
                    };
                    return response;
                }
            }
            
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