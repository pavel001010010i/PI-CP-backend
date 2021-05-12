using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaTM.DB.Model.Models
{
    [Table("Cargos")]
    public class Cargo
    {
        [Key]
        public int Id { get; set; }
        public string IdUser { get; set; }
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
        public int? OrderDataId { get; set; }

        public bool isStatus { get; set; }

        [ForeignKey("IdUser")]
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<TypeCargo> TypeCargo { get; set; }
        public int IdRouteMap { get; set; }
        [ForeignKey("IdRouteMap")]
        public virtual RouteMap RouteMap { get; set; }
        public int IdTypePayment { get; set; }
        [ForeignKey("IdTypePayment")]
        public virtual TypePayment TypePayment{ get; set; }

        public int IdTypeCurrency { get; set; }
        [ForeignKey("IdTypeCurrency")]
        public virtual TypeCurrency TypeCurrency { get; set; }

    }
    
}