using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AviaTM.Models
{
    public class User
    {
        [Key]

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Email")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Lockout Enable")]
        public bool LockoutEnable { get; set; }

    }

    public class RequestUser
    {
        [Key]
        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Email")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class UserBody
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}