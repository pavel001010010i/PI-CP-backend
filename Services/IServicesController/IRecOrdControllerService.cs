using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AviaTM.Services.IServicesController
{
    public interface IRecOrdControllerService
    {
        IEnumerable<RequestMain> GetRequests();
        IEnumerable<RequestMain> GetRequestsUserCustomer(string idUser);
        IEnumerable<RequestMain> GetRequestsUserProvider(string idUser);
        Task<RequestMain> GetRequestMainId(int id);
        Task<ResponseMessageModel> Add(RequestModel model);
        Task Update(RequestModel model);


        Task<ResponseMessageModel> DeleteRequestCustomer(RequestMain model);

    }
}
