using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaTM.DB.Model.Models
{
    public class Transport
    {
        public int Id { get; set; }

        public string IdUser { get; set; }
        public int IdTypeTransport { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string Model { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Depth { get; set; }
        public int NumberAxes { get; set; }

        [Required]
        public int MaxLoadCapacity { get; set; }

        [Required]
        public double FuelConsumption { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("IdUser")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("IdTypeTransport")]
        public virtual TypeTransport TypeTransport { get; set; }
        public int IdTransLoadCapacity { get; set; }

        [ForeignKey("IdTransLoadCapacity")]
        public virtual TransportLoadCapacity TransportLoadCapacity { get; set; }
        public int IdRouteMap { get; set; }

        [ForeignKey("IdRouteMap")]
        public virtual RouteMap RouteMap { get; set; }

    }
}
