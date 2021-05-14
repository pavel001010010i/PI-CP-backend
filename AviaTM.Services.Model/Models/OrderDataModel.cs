using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AviaTM.Services.Models.Models
{
    public class OrderDataModel
    {
        public string IdUser { get; set; }
        public bool Status { get; set; }
        public ICollection<Cargo> Cargoes { get; set; }

    }
}
