using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AviaTM.Services.Models.Models
{
    public class CargoModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdTypePayment { get; set; }
        public int IdTypeCurrency { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Depth { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int CostDelivery { get; set; }
        [Required]
        public bool isStatus { get; set; }

        public ICollection<TypeCargo> TypeCargo { get; set; }

    }
}
