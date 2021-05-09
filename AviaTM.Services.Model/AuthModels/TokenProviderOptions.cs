using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.Models.Models
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public Func<string, string, Task<ClaimsIdentity>> IdentityResolver { get; set; }

        public Func<Task<string>> NonceGenerator { get; set; } = () => Task.FromResult(Guid.NewGuid().ToString());
    }
}
