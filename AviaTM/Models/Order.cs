using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AviaTM.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        [ForeignKey("Cargo")]
        public int CargoId { get; set; }
        [ForeignKey("Plane")]
        public int PlaneId { get; set; }
        [Required]
        [ForeignKey("Country")]
        public string CountryIdTo { get; set; }
        [Required]
        [ForeignKey("Country2")]
        public string CountryIdFrom { get; set; }
        [Required]
        public DateTime DateDeparture { get; set; }
        [Required]
        public DateTime DateDelivery { get; set; }
        public string Status { get; set; }
        public double CastDelivery { get; set; }

        public virtual Plane Plane { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Country Country { get; set; }
        public virtual Country Country2 { get; set; }

        public virtual Cargo Cargo { get; set; }
    }
}