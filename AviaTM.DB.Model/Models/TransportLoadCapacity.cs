using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaTM.DB.Model.Models
{
    public class TransportLoadCapacity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<Transport> Transports { get; set; }

    }
}
