using AviaTM.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class OrderData
    {

        public int Id { get; set; }
        public int IdOrder{ get; set; }
        public string IdUser{ get; set; }
        public int IdInfoTransfer { get; set; }
        public int IdTypeUser { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        //[Required]
        public bool Status { get; set; }
        [ForeignKey("IdUser")]
        public  AppUser AppUser { get; set; }

        [ForeignKey("IdOrder")]
        public  OrderMain Order{ get; set; }

        [ForeignKey("IdInfoTransfer")]
        public InfoTransfer InfoTransfer { get; set; }

        [ForeignKey("IdTypeUser")]
        public TypeUser TypeUser{ get; set; }

    }
}
