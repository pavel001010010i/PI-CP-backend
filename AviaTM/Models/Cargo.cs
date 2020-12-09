using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaTM.Models
{
    [Table("Cargos")]
    public class Cargo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cargo Name")]
        public string Name { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Depth { get; set; }
        [Required]
        public int Weight { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
    public class CargoBody
    {
        [Key]
        [Display(Name = "Cargo Name")]
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
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
    }
}