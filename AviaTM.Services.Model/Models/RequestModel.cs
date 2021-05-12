using AviaTM.DB.Model.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AviaTM.Services.Models.Models
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int IdTransport { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public OrderData OrderData { get; set; }

    }
}
