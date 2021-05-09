using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviaTM.Services.Models.Infastructure
{
    public class UserContext
    {
        public string UserId { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public string Email { get; set; }

        public bool IsInRole(string role)
        {
            return Roles?.Contains(role) ?? false;
        }
    }
}
