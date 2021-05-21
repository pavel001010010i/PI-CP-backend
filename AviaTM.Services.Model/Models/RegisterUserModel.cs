using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.Models.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Заполните поле c контактным лицом")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните поле с названием организации")]
        public string NameOrganization { get; set; }
        public string UNP { get; set; }
        public string AnotherContact { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Заполните поле с моб. номером")]
        [Phone(ErrorMessage ="Формат Моб. телефона не верный!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Заполните поле ЛОГИН")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Заполните поле с Емайлом")]
        [EmailAddress(ErrorMessage = "Формат Емайла не верный")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
