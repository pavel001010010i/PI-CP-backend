using AviaTM;
using AviaTM.DB.IRepository;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Infastructure;
using AviaTM.Services.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTm.DB.Repository
{
    public class TransportRepository : ITransportRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContext _userContext;

        public TransportRepository(ApplicationDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public IEnumerable<Transport> GetTransports()
        {
            var transports = _context.Transports.Where(x => x.IdUser.Contains(_userContext.UserId)).OrderByDescending(x => x.Id)
                .Select(x => new Transport
                {
                    Id = x.Id,
                    Name = x.Name,
                    Model = x.Model,
                    Width = x.Width,
                    Height = x.Height,
                    Depth = x.Depth,
                    NumberAxes = x.NumberAxes,
                    MaxLoadCapacity = x.MaxLoadCapacity,
                    FuelConsumption = x.FuelConsumption,
                    IsActive = x.IsActive,
                    IdUser = x.IdUser,
                    IdTypeTransport = x.IdTypeTransport,
                    IdTransLoadCapacity = x.IdTransLoadCapacity,
                    IdRouteMap = x.IdRouteMap,
                    TransportLoadCapacity = x.TransportLoadCapacity,
                    TypeTransport = x.TypeTransport,
                    RouteMap = new RouteMap
                    {
                        CountryCodeFrom = x.RouteMap.CountryCodeFrom,
                        CountryCodeTo = x.RouteMap.CountryCodeTo,
                        CityFrom = x.RouteMap.CityFrom,
                        CityTo = x.RouteMap.CityTo,
                        latFrom = x.RouteMap.latFrom,
                        lngFrom = x.RouteMap.lngFrom,
                        latTo = x.RouteMap.latTo,
                        lngTo = x.RouteMap.lngTo,
                        CountyFrom = x.RouteMap.CountyFrom,
                        CountyTo = x.RouteMap.CountyTo,
                        FullAddressFrom = x.RouteMap.FullAddressFrom,
                        FullAddressTo = x.RouteMap.FullAddressTo,
                        Id = x.RouteMap.Id,
                        EndDate = x.RouteMap.EndDate,
                        StartDate = x.RouteMap.StartDate
                    }
                });

            return transports;
        }

        public async Task AddTransport(RouteModel model)
        {
            var RouteMap = new RouteMap()
            {
                CountryCodeFrom = model.CountryCodeFrom,
                latFrom = model.latFrom,
                lngFrom = model.lngFrom,
                CountyFrom = model.CountyFrom,
                CityFrom = model.CityFrom,
                StateFrom = model.StateFrom,
                StreetFrom = model.StreetFrom,
                PostCodeFrom = model.PostCodeFrom,
                FullAddressFrom = model.FullAddressFrom,
                CountryCodeTo = model.CountryCodeTo,
                latTo = model.latTo,
                lngTo = model.lngTo,
                CountyTo = model.CountyTo,
                CityTo = model.CityTo,
                StateTo = model.StateTo,
                StreetTo = model.StreetTo,
                PostCodeTo = model.PostCodeTo,
                FullAddressTo = model.FullAddressTo,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };
            _context.RouteMaps.Add(RouteMap);
            await _context.SaveChangesAsync();

            var transport = new Transport()
            {
                IdUser = model.Transport.IdUser,
                Name = model.Transport.Name,
                Model = model.Transport.Model,
                Height = model.Transport.Height,
                Width = model.Transport.Width,
                Depth = model.Transport.Depth,
                NumberAxes = model.Transport.NumberAxes,
                MaxLoadCapacity = model.Transport.MaxLoadCapacity,
                FuelConsumption = model.Transport.FuelConsumption,
                IsActive = model.Transport.IsActive,
                IdRouteMap = RouteMap.Id,
                IdTypeTransport = model.Transport.IdTypeTransport,
                IdTransLoadCapacity = model.Transport.IdTransLoadCapacity
            };

            _context.Transports.Add(transport);
            await _context.SaveChangesAsync();
        }

        public async Task<ResponseMessageModel> DeleteTransport(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            var orderMain = await _context.OrderMain.FirstOrDefaultAsync(x => x.IdTransport == id);
            var requestMain = await _context.RequestMain.FirstOrDefaultAsync(x => x.IdTransport == id);

            if(requestMain!=null && orderMain != null)
            {
                var orderDataRequest = _context.OrderData.Include(x=>x.Cargoes)
                    .FirstOrDefault(x => x.RequestMainId == requestMain.Id);
                    
                var orderDataOrder = _context.OrderData.Include(x=>x.Cargoes)
                    .FirstOrDefault(x => x.OrderMainId == orderMain.Id);

                foreach (var cargo in orderDataRequest.Cargoes)
                {
                    cargo.OrderDataId = null;
                    _context.Cargoes.Update(cargo);
                }
                foreach (var cargo in orderDataOrder.Cargoes)
                {
                    cargo.OrderDataId = null;
                    _context.Cargoes.Update(cargo);
                }

                _context.OrderData.Remove(orderDataRequest);
                _context.OrderData.Remove(orderDataOrder);
                await _context.SaveChangesAsync();

                _context.OrderMain.Remove(orderMain);
                _context.RequestMain.Remove(requestMain);
                await _context.SaveChangesAsync();

                _context.Transports.Remove(transport);
                await _context.SaveChangesAsync();

                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Транспорт удален!"
                };
            }

            if (requestMain != null)
            {
                var orderDataRequest = _context.OrderData.Include(x =>x.Cargoes)
                    .FirstOrDefault(x => x.RequestMainId == requestMain.Id);

                foreach (var cargo in orderDataRequest.Cargoes)
                {
                    cargo.OrderDataId = null;
                    _context.Cargoes.Update(cargo);
                }

                _context.OrderData.Remove(orderDataRequest);
                
                await _context.SaveChangesAsync();

                _context.RequestMain.Remove(requestMain);
                await _context.SaveChangesAsync();

                _context.Transports.Remove(transport);
                await _context.SaveChangesAsync();

                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Транспорт удален!"
                };
            }
            if (orderMain != null)
            {
                var orderDataOrder = _context.OrderData.Include(x=>x.Cargoes)
                    .FirstOrDefault(x => x.OrderMainId == orderMain.Id);

                foreach (var cargo in orderDataOrder.Cargoes)
                {
                    cargo.OrderDataId = null;
                    _context.Cargoes.Update(cargo);
                }

                _context.OrderData.Remove(orderDataOrder);
                await _context.SaveChangesAsync();

                _context.OrderMain.Remove(orderMain);
                await _context.SaveChangesAsync();

                _context.Transports.Remove(transport);
                await _context.SaveChangesAsync();

                return new ResponseMessageModel
                {
                    Status = true,
                    Message = "Транспорт удален!"
                };
            }
            return new ResponseMessageModel
            {
                Status = false,
                Message = "Транспорт не удален!"
            };

        }

        public async Task UpdateTransport(RouteModel model)
        {
            var transport = _context.Transports.FirstOrDefault(x => x.Id == model.Transport.Id);
            var routeMap = _context.RouteMaps.FirstOrDefault(x => x.Id == model.Id);

            _context.RouteMaps.Attach(routeMap);
            _context.Transports.Attach(transport);


            routeMap.CountryCodeFrom = model.CountryCodeFrom;
            routeMap.latFrom = model.latFrom;
            routeMap.lngFrom = model.lngFrom;
            routeMap.CityFrom = model.CityFrom;
            routeMap.CountyFrom = model.CountyFrom;
            routeMap.StateFrom = model.StateFrom;
            routeMap.StreetFrom = model.StreetFrom;
            routeMap.PostCodeFrom = model.PostCodeFrom;
            routeMap.FullAddressFrom = model.FullAddressFrom;
            routeMap.CountryCodeTo = model.CountryCodeTo;
            routeMap.latTo = model.latTo;
            routeMap.lngTo = model.lngTo;
            routeMap.CityTo = model.CityTo;
            routeMap.CountyTo = model.CountyTo;
            routeMap.StateTo = model.StateTo;
            routeMap.StreetTo = model.StreetTo;
            routeMap.PostCodeTo = model.PostCodeTo;
            routeMap.FullAddressTo = model.FullAddressTo;
            routeMap.StartDate = model.StartDate;
            routeMap.EndDate = model.EndDate;

            _context.RouteMaps.Update(routeMap);

            transport.Name = model.Transport.Name;
            transport.Model = model.Transport.Model;
            transport.Height = model.Transport.Height;
            transport.Width = model.Transport.Width;
            transport.Depth = model.Transport.Depth;
            transport.NumberAxes = model.Transport.NumberAxes;
            transport.MaxLoadCapacity = model.Transport.MaxLoadCapacity;
            transport.FuelConsumption = model.Transport.FuelConsumption;
            transport.IsActive = model.Transport.IsActive;
            transport.IdTransLoadCapacity = model.Transport.IdTransLoadCapacity;
            transport.IdTypeTransport = model.Transport.IdTypeTransport;

            _context.Transports.Update(transport);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transport>> SearchTransport(SearchtModel model)
        {
            var transports =  _context.Transports
                .Include(x=>x.TransportLoadCapacity)
                .Include(x=>x.TypeTransport)
                .Include(x=>x.RouteMap)
                .Include(x=>x.AppUser)
                .Where(x=>x.IsActive)
                .AsQueryable().AsNoTracking();

            if (model == null)
            {
                return transports.ToList();
            }

            if (model.DateOf != null)
            {
                transports = transports.Where(x => x.RouteMap.StartDate >= model.DateOf);
            }
            if (model.DateTo != null)
            {
                transports = transports.Where(x => x.RouteMap.EndDate <= model.DateTo);
            }

            if (model.HeightOf != null)
            {
                transports = transports.Where(x => x.Height >= model.HeightOf);
            }
            if (model.HeightTo != null)
            {
                transports = transports.Where(x => x.Height <= model.HeightTo);
            }

            if (model.WidthOf != null)
            {
                transports = transports.Where(x => x.Width >= model.WidthOf);
            }
            if (model.WidthTo != null)
            {
                transports = transports.Where(x => x.Width <= model.WidthTo);
            }

            if (model.DepthOf != null)
            {
                transports = transports.Where(x => x.Depth >= model.DepthOf);
            }
            if (model.DepthTo != null)
            {
                transports = transports.Where(x => x.Depth <= model.DepthTo);
            }

            if (model.IdTypeTransport != null)
            {
                transports = transports.Where(x => x.IdTypeTransport == model.IdTypeTransport);
            }

            if (model.WeightOf != null)
            {
                transports = transports.Where(x => x.MaxLoadCapacity >= model.WeightOf);
            }

            if (model.WeightTo!= null)
            {
                transports = transports.Where(x => x.MaxLoadCapacity <= model.WeightTo);
            }

            if (model.CountryOf != null)
            {
                transports = transports.Where(x => x.RouteMap.CountyFrom.ToUpper().Contains(model.CountryOf.ToUpper()));
            }
            if (model.CountryTo != null)
            {
                transports = transports.Where(x => x.RouteMap.CountyTo.ToUpper().Contains(model.CountryTo.ToUpper()));
            }

            if (model.CityOf != null)
            {
                transports = transports.Where(x => x.RouteMap.CityFrom.ToUpper().Contains(model.CityOf.ToUpper()));
            }
            if (model.CityTo != null)
            {
                transports = transports.Where(x => x.RouteMap.CityTo.ToUpper().Contains(model.CityTo.ToUpper()));
            }

            if (model.StateOf != null)
            {
                transports = transports.Where(predicate: x => x.RouteMap.StateFrom.ToUpper().Contains(model.StateOf.ToUpper()));
            }

            if (model.StateTo != null)
            {
                transports = transports.Where(x => x.RouteMap.StateTo.ToUpper().Contains(model.StateTo.ToUpper()));
            }

            if (model.PostcodeOf != null)
            {
                transports = transports.Where(x => x.RouteMap.PostCodeFrom.ToUpper().Contains(model.PostcodeOf.ToUpper()));
            }

            if (model.PostcodeTo != null)
            {
                transports = transports.Where(x => x.RouteMap.PostCodeTo.ToUpper().Contains(model.PostcodeTo.ToUpper()));
            }
            return await transports.OrderByDescending(x=>x.Id).ToListAsync();
        }
    }
}
