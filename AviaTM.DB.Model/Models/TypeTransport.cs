using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class TypeTransport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public bool isActive { get; set; }

        public virtual ICollection<TypeCargo> TypeCargos{ get; set; }
    }
}
