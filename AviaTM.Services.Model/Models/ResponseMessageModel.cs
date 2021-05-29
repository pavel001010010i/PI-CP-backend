using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.Models.Models
{
    public class ResponseMessageModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ResponseMessageUserModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public AppUser User {get; set;}
    }
    public class ResponseConfirmEmailModel
    {
        public string Code { get; set; }
    }
}
