using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.DB.Model.Models
{
    public class TypeCurrency
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<Cargo> Cargos { get; set; }
    }
}
