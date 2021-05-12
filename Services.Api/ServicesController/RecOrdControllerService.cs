using AviaTM;
using AviaTM.DB.Model.Models;
using AviaTM.DB.Model;
using AviaTM.Services.IServicesController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AviaTM.Services.Models.Models;
using Microsoft.EntityFrameworkCore;
using AviaTM.Services.Models.Infastructure;

namespace AviaTm.Services.Api.ServicesController
{
    public class RecOrdControllerService : IRecOrdControllerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContext _userContext;

        public RecOrdControllerService(ApplicationDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        

        public IEnumerable<RequestMain> GetRequests()
        {
            return _context.RequestMain.ToList();
        }

        public async Task<RequestMain> GetRequestMainId(int id)
        {
            return await _context.RequestMain.FindAsync(id);
        }

        public async Task<ResponseMessageModel> Add(RequestModel model)
        {
            foreach(var cargo in model.OrderData.Cargoes)
            {
                var existOrderDats = _context.RequestMain.Where(x => x.OrderDats.Any(x => x.Cargoes.Contains(cargo))).AsNoTracking();
                if (existOrderDats.AsNoTracking() != null && existOrderDats.Any())
                {
                    return new ResponseMessageModel
                    {
                        Status = false,
                        Message = "Грузы или один из грузов уже оформлены в заявке!"
                    };
                }
            }
            
            var requestItem = _context.RequestMain.Include(x=>x.OrderDats).FirstOrDefault(x => x.IdTransport == model.IdTransport);
            if (requestItem != null)
            {
                _context.Cargoes.AttachRange(model.OrderData.Cargoes);
                _context.OrderData.Add(model.OrderData);

                _context.RequestMain.Attach(requestItem);
                requestItem.OrderDats.Add(model.OrderData);
                await _context.SaveChangesAsync();

                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Заявка добавлена!"
                };
            }
            else
            {
                _context.Cargoes.AttachRange(model.OrderData.Cargoes);

                _context.OrderData.Add(model.OrderData);
                await _context.SaveChangesAsync();

                _context.RequestMain.Add(new RequestMain 
                {
                    IdUser = model.IdUser,
                    IdTransport = model.IdTransport,
                    Name = model.Name,
                    Status = model.Status,
                    OrderDats = new List<OrderData>() { model.OrderData }
                });
                await _context.SaveChangesAsync();

                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Заявка добавлена!"
                };
            }
            
        }

        public async Task Update(RequestModel model)
        {
            //доделать
            var req = new RequestMain
            {
                IdUser = model.IdUser,
                IdTransport = model.IdTransport,
                Name = model.Name,
                Status = model.Status,
                OrderDats = new List<OrderData>() { model.OrderData }
            };
            _context.Attach(req);
            _context.RequestMain.Update(req);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<RequestMain> GetRequestsUserCustomer(string idUser)
        {
            return _context.RequestMain.Where(x => x.OrderDats.Where(x => x.IdUser == idUser).Any())
                .Select(x => new RequestMain
                {
                    Name = x.Name,
                    IdUser = x.IdUser,
                    IdTransport = x.IdTransport,
                    Transport = new Transport
                    {
                        
                        Name = x.Transport.Name,
                        Model = x.Transport.Model,
                        RouteMap = new RouteMap
                        {
                            FullAddressFrom = x.Transport.RouteMap.FullAddressFrom,
                            FullAddressTo = x.Transport.RouteMap.FullAddressTo,
                            EndDate = x.Transport.RouteMap.EndDate,
                            StartDate = x.Transport.RouteMap.StartDate
                        },
                        Height = x.Transport.Height,
                        Width = x.Transport.Width,
                        Depth = x.Transport.Depth,
                        MaxLoadCapacity = x.Transport.MaxLoadCapacity
                        
                    },
                    OrderDats = x.OrderDats.Where(x=>x.IdUser.Contains(idUser)).Select(x => new OrderData
                    {
                        Id = x.Id,
                        IdUser = x.User.Id,
                        OrderMainId = x.OrderMainId,
                        RequestMainId = x.RequestMainId,
                        Cargoes = x.Cargoes.Select(x =>new Cargo
                        {
                            Id = x.Id,
                            AppUser = x.AppUser,
                            IdUser = x.IdUser,
                            Name = x.Name,
                            RouteMap = x.RouteMap,
                            CostDelivery = x.CostDelivery,
                            TypeCargo = x.TypeCargo,
                            TypePayment = x.TypePayment,
                            TypeCurrency = x.TypeCurrency,
                            Height = x.Height,
                            Weight = x.Weight,
                            Depth = x.Depth,
                            Width = x.Width,
                            isStatus= x.isStatus,
                            IdTypeCurrency = x.IdTypeCurrency,
                            IdTypePayment = x.IdTypePayment,
                            OrderDataId =x.OrderDataId
                        }).ToList(),
                        
                    }).ToList(),
                   
                }).AsNoTracking();              
        }

        public IEnumerable<RequestMain> GetRequestsUserProvider(string idUser)
        {
            return _context.RequestMain.Where(x => x.IdUser == idUser);
        }

        public async Task<ResponseMessageModel> DeleteRequestCustomer(RequestMain model)
        {
            foreach(var cargoes in model.OrderDats.Select(x=>x.Cargoes))
            {
                foreach (var cargo in cargoes)
                {
                    var car = new Cargo
                    {
                        Id = cargo.Id,
                        Name = cargo.Name,
                        RouteMap = cargo.RouteMap,
                        CostDelivery = cargo.CostDelivery,
                        Height = cargo.Height,
                        Weight = cargo.Weight,
                        Depth = cargo.Depth,
                        Width = cargo.Width,
                        isStatus = cargo.isStatus,
                        IdTypeCurrency = cargo.IdTypeCurrency,
                        IdTypePayment = cargo.IdTypePayment,
                        OrderDataId = cargo.OrderDataId,
                        IdUser = cargo.IdUser
                    };
                    _context.Cargoes.Attach(car);
                    car.OrderDataId = null;
                    _context.Cargoes.Update(car);
                   
                }
            }

            var requestMainId = model.OrderDats.Select(x => x.RequestMainId).FirstOrDefault();
            var ordersData = _context.OrderData.Where(x => x.RequestMainId == requestMainId && x.IdUser == _userContext.UserId);

            _context.OrderData.RemoveRange(ordersData);
            await _context.SaveChangesAsync();

            var ordersDataForRequestIdExist = _context.OrderData.Where(x => x.RequestMainId == requestMainId).ToList();
            if (!ordersDataForRequestIdExist.Any())
            {
                var reqMain = _context.RequestMain.FirstOrDefault(x => x.Id == requestMainId);
                _context.RequestMain.Remove(reqMain);
            }
            await _context.SaveChangesAsync();


            return new ResponseMessageModel
            {
                Status = true,
                Message = "Ваша заявка на доставку отменена!"
            };
        }
    }
}
