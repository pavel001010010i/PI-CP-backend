using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface ITransportControllerService
    {
        Task<IEnumerable<Transport>> GetTransports();
        Task AddTransport(RouteModel model);
        Task<ResponseMessageModel> DeleteTransport(int id);
        Task UpdateTransport(RouteModel model);
        Task<IEnumerable<Transport>> SearchTransport(SearchtModel model);
    }
}
