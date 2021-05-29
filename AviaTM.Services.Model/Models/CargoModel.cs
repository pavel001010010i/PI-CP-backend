using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AviaTM.Services.Models.Models
{
    public class CargoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Тип груза' ")]
        public string IdUser { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Тип груза' ")]
        public int IdTypePayment { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Тип груза' ")]
        public int IdTypeCurrency { get; set; }
        [Required(ErrorMessage = "Заполните поле наименованием груза")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Высота' ")]
        public int Height { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Широта' ")]
        public int Width { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Глубина' ")]
        public int Depth { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Вес' ")]
        public int Weight { get; set; }
        [Required(ErrorMessage = "Заполните поле 'Стоимость' ")]
        public int CostDelivery { get; set; }
        [Required]
        public bool isStatus { get; set; }

        [Required(ErrorMessage = "Заполните поле 'Тип груза' ")]
        public ICollection<TypeCargo> TypeCargo { get; set; }
    }
}
