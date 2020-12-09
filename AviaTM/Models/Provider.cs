using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AviaTM.Models
{
    [Table("Providers")]
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        [Required]
        public string NameCompany{ get; set; }
        [Required]
        public string LicenceNumber { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public string CountresProvider { get; set; }


    }
    public class ProviderBody
    {
        public int ProviderId { get; set; }
        [Required]
        [Display(Name = "Name Company")]
        public string NameCompany { get; set; }
        [Required]
        [Display(Name = "Licence Number")]
        public string LicenceNumber { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Countres Provider")]
        public string CountresProvider { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]
        public string PasswordConfirm { get; set; }


    }
}