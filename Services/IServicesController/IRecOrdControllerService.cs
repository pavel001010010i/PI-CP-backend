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

        IEnumerable<RequestMain> GetOrdersUserCustomer(string idUser);
        IEnumerable<RequestMain> GetOrdersUserProvider(string idUser);
        Task<ResponseMessageModel> DeleteOrderCustomer(RequestMain model);
        Task<ResponseMessageModel> DeleteOrderProvider(RequestMain model);


        Task<ResponseMessageModel> DeleteRequestCustomer(RequestMain model);
        Task<ResponseMessageModel> DeleteRequestProvider(RequestMain model);
        Task<ResponseMessageModel> AcceptItemRequest(RequestMain model);
        Task<ResponseMessageModel> DoneItemOrder(RequestMain model);


    }
}
