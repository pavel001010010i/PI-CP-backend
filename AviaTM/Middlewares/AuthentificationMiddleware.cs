using AviaTM.Services.Models.Infastructure;
using AviaTM.Services.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Middlewares
{
    public class AuthentificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthorizationSettings _authSettings;
        public AuthentificationMiddleware(RequestDelegate next, IOptions<AuthorizationSettings> authSettings)
        {
            _next = next;
            _authSettings = authSettings.Value;
        }

        public async Task Invoke(HttpContext context, UserContext userContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.SecretKey));

                var tokenValidationParameters = GetValidationParameters(key);

                bool isValidToken = IsValidToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                if (!isValidToken)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                var jwtToken = (JwtSecurityToken)validatedToken;
                SetUserContext(jwtToken, userContext);

                var identity = new ClaimsIdentity(jwtToken.Claims, "custom", "name", "roles");
                context.User = new ClaimsPrincipal(identity);
            }

            await _next(context);
        }

        private TokenValidationParameters GetValidationParameters(SymmetricSecurityKey key)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _authSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _authSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
        private bool IsValidToken(string token, TokenValidationParameters tokenValidationParameters, out SecurityToken validatedToken)
        {
            try
            {
                new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out validatedToken);
            }
            catch (SecurityTokenException ex)
            {
                validatedToken = null;
                return false;
            }

            return true;
        }

        private void SetUserContext(JwtSecurityToken jwtToken, UserContext userContext)
        {
            userContext.UserId = jwtToken.Claims.FirstOrDefault(
                x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            userContext.Roles = jwtToken.Claims.Where(x => x.Type == "roles").Select(x => x.Value);
            userContext.Email = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
        }
    }
}
