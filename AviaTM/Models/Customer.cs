using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AviaTM.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Range(18, 90, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
        [Required]
        public string PassportData { get; set; }
        [Required]
        public string Sex { get; set; }

    }
    public class CustomerBody
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Range(18, 90, ErrorMessage = "Invalid age")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required]
        [Display(Name = "Passport Data")]
        public string PassportData { get; set; }
        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }
        [Required]
        [Display(Name = "Role")]    
        public string Role { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]
        public string PasswordConfirm { get; set; }


    }
}