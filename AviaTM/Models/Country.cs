using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AviaTM.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        [Required]
        public string NameCountry { get; set; }
        [Required]
        [Range(-90, 90, ErrorMessage = "Invalid latitude")]
        public int Latitude { get; set; }
        [Required]
        [Range(-180, 180, ErrorMessage = "Invalid longitude")]
        public int Longitude { get; set; }
        [Required]
        public string Index { get; set; }


        //public virtual ICollection<Cargo> Cargos { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }

    }
}