using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaTM.DB.Model.Models
{
    public class RequestMain
    {
        public int Id { get; set; }
        public int IdTransport { get; set; }
        public string IdUser { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        public bool Status { get; set; }

        [ForeignKey("IdTransport")]
        public virtual Transport Transport { get; set; }

        [ForeignKey("IdUser")]
        public virtual AppUser User { get; set; }
        public virtual ICollection<OrderData> OrderDats{ get; set; }

    }
}
