using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class OrderData
    {

        public int Id { get; set; }
        public int? RequestMainId { get; set; }
        public int? OrderMainId { get; set; }

        public string IdUser{ get; set; }

        public bool Status { get; set; }
        [ForeignKey("RequestMainId")]
        public virtual RequestMain RequestMain{ get; set; }
        [ForeignKey("OrderMainId")]
        public virtual OrderMain OrderMain { get; set; }

        [ForeignKey("IdUser")]
        public virtual AppUser User { get; set; }
        public virtual ICollection<Cargo> Cargoes { get; set; }


    }
}
