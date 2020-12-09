using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AviaTM.Models
{
    [Table("Planes")]
    public class Plane
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Provider")]
        public int IdProvider { get; set; }
        public string NamePlane { get; set; }
        public string ModelPlane { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int depth { get; set; }
        public int CapacityWeight { get; set; }

        public virtual Provider Provider { get; set; }
        //public virtual IEnumerable<Order> Orders { get; set; }

    }
    public class PlaneBody
    {
        [Key]
        public string NameCompany { get; set; }
        [Required]
        public string NamePlane { get; set; }
        [Required]
        public string ModelPlane { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int depth { get; set; }
        [Required]
        public int CapacityWeight { get; set; }

    }
}