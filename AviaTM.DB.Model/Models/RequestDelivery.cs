using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AviaTM.Db.Models
{
    [Table("RequestDeliveries")]
    public class RequestDelivery
    {
        [Key]
        public int IdRequest { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Provider")]
        public int ProviderId { get; set; }
        [ForeignKey("Cargo")]
        public int CargoId { get; set; }
        [ForeignKey("Country")]
        public string CountryIdTo { get; set; }
        [Required]
        [ForeignKey("Country2")]
        public string CountryIdFrom { get; set; }
        [Required]
        public DateTime DateDeparture { get; set; }
        [Required]
        public DateTime DateDelivery { get; set; }
        [Required]
        public bool StatusRequest { get; set; }
        [Required]
        public double CastDelivery { get; set; }

        public virtual Cargo Cargo { get; set; }
    }
    public class RDBody
    {
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public string CargoName { get; set; }
        [Required]
        public string CountryNameTo { get; set; }
        [Required]
        public string CountryNameFrom{ get; set; }
        [Required]
        public DateTime dateDep { get; set; }
        [Required]
        public DateTime dateDel { get; set; }
        [Required]
        public bool StatusRequest { get; set; }
        [Required]
        public double castDep { get; set; }
    }

}
