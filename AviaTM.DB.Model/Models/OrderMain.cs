using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class OrderMain
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Status { get; set; }
        public virtual ICollection<OrderData> OrderDatas{ get; set; }

    }
}
