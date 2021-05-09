using AviaTM.DB.Model.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.DB.Model.Models
{
    public class AppUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string NameOrganization { get; set; }
        public string UNP { get; set; }
        public string AnotherContact { get; set; }
        public string Address { get; set; }
        public bool isLockdown { get; set; }

    }

    public class UserBody
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
