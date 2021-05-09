using AviaTM.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class InfoTransfer
    {
        public int Id { get; set; }
        public int IdTransport { get; set; }
        public int IdCargo { get; set; }
        public int IdRoute { get; set; }
        [ForeignKey("IdTransport")]
        public virtual Transport Transport{ get; set; }
        [ForeignKey("IdCargo")]
        public virtual Cargo Cargo { get; set; }
        [ForeignKey("IdRoute")]
        public virtual RouteMap RouteMap{ get; set; }
        public virtual  ICollection<OrderData> OrderDatas{ get; set; }
        

    }
}
