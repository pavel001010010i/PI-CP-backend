﻿using AviaTM;
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
    public class CargoRepository : ICargoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContext _userContext;

        public CargoRepository(ApplicationDbContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        public async Task AddCargos(RouteModel model)
        {
            _context.TypeCargoes.AttachRange(model.Cargo.TypeCargo);
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

            var cargo = new Cargo()
            {
                IdUser = model.Cargo.IdUser,
                Height = model.Cargo.Height,
                Width = model.Cargo.Width,
                Weight = model.Cargo.Weight,
                CostDelivery = model.Cargo.CostDelivery,
                Depth = model.Cargo.Depth,
                isStatus = model.Cargo.isStatus,
                Name = model.Cargo.Name,
                TypeCargo = model.Cargo.TypeCargo,
                IdRouteMap = RouteMap.Id,
                IdTypeCurrency = model.Cargo.IdTypeCurrency,
                IdTypePayment = model.Cargo.IdTypePayment
            };

            _context.Cargoes.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Cargo> GetCargos()
        {
            var cargoes = _context.Cargoes
                .Include(x => x.TypeCurrency)
                .Include(x => x.TypePayment)
                .Include(x=>x.TypeCargo)
                .Include(x => x.RouteMap).Where(x => x.IdUser.Contains(_userContext.UserId)).OrderByDescending(x => x.Id)
                .AsNoTracking();
            return cargoes;
        }
        public IEnumerable<Cargo> GetCargoesForSelectRequest()
        {
            var cargoes = _context.Cargoes
                .Where(x => x.IdUser.Contains(_userContext.UserId))
                .OrderByDescending(x => x.Id)
                .AsNoTracking();
            return cargoes;
        }

        public async Task DeleteCargo(int id)
        {
            var cargo = await _context.Cargoes.FindAsync(id);

            _context.Cargoes.Remove(cargo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCargo(RouteModel model)
        {
            _context.TypeCargoes.AttachRange(model.Cargo.TypeCargo);
            var cargo = _context.Cargoes.Include(x=>x.TypeCargo).FirstOrDefault(x=>x.Id == model.Cargo.Id);
            var routeMap = _context.RouteMaps.FirstOrDefault(x => x.Id == model.Id);

            _context.RouteMaps.Attach(routeMap);
            _context.Cargoes.Attach(cargo);


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
            routeMap.lngTo= model.lngTo;
            routeMap.CityTo = model.CityTo;
            routeMap.CountyTo = model.CountyTo;
            routeMap.StateTo = model.StateTo;
            routeMap.StreetTo = model.StreetTo;
            routeMap.PostCodeTo = model.PostCodeTo;
            routeMap.FullAddressTo = model.FullAddressTo;
            routeMap.StartDate = model.StartDate;
            routeMap.EndDate = model.EndDate;

            _context.RouteMaps.Update(routeMap);

            cargo.Name = model.Cargo.Name;
            cargo.Height = model.Cargo.Height;
            cargo.Width = model.Cargo.Width;
            cargo.Depth = model.Cargo.Depth;
            cargo.Weight = model.Cargo.Weight;
            cargo.CostDelivery = model.Cargo.CostDelivery;
            cargo.isStatus = model.Cargo.isStatus;
            cargo.TypeCargo = model.Cargo.TypeCargo;
            cargo.IdTypeCurrency = model.Cargo.IdTypeCurrency;
            cargo.IdTypePayment = model.Cargo.IdTypePayment;

            _context.Cargoes.Update(cargo); 
            
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cargo>> SearchCargo(SearchtModel model)
        {

            var cargoes = _context.Cargoes
                .Where(x => x.isStatus)
                .Select(x => new Cargo {
                    Id = x.Id,
                    IdRouteMap = x.IdRouteMap,
                    IdTypeCurrency = x.IdTypeCurrency,
                    IdTypePayment = x.IdTypePayment,
                    IdUser = x.IdUser,
                    OrderDataId = x.OrderDataId,
                    Height = x.Height,
                    Weight = x.Weight,
                    Depth = x.Depth,
                    CostDelivery = x.CostDelivery,
                    Width = x.Width,
                    Name = x.Name,
                    TypePayment = new TypePayment
                    {
                        Id = x.TypePayment.Id,
                        Name = x.TypePayment.Name,
                    },
                    TypeCurrency = new TypeCurrency
                    {
                        Id = x.TypeCurrency.Id,
                        Name = x.TypeCurrency.Name
                    },
                    TypeCargo =  x.TypeCargo,
                    AppUser = new AppUser
                    {
                        Id = x.AppUser.Id,
                        Email = x.AppUser.Email,
                        PhoneNumber = x.AppUser.PhoneNumber,
                        NameOrganization = x.AppUser.NameOrganization,
                        Address = x.AppUser.Address
                    },
                    RouteMap = new RouteMap
                    {
                        Id = x.RouteMap.Id,
                        FullAddressFrom = x.RouteMap.FullAddressFrom,
                        FullAddressTo = x.RouteMap.FullAddressTo,
                        StartDate = x.RouteMap.StartDate,
                        EndDate = x.RouteMap.EndDate,
                        CountryCodeFrom = x.RouteMap.CountryCodeFrom,
                        CountryCodeTo = x.RouteMap.CountryCodeTo,
                        PostCodeFrom = x.RouteMap.PostCodeFrom,
                        PostCodeTo = x.RouteMap.PostCodeTo,
                        CountyFrom = x.RouteMap.CountyFrom,
                        CountyTo  = x.RouteMap.CountyTo,
                        StateFrom = x.RouteMap.StateFrom,
                        StateTo = x.RouteMap.StateTo,
                        CityFrom = x.RouteMap.CityFrom,
                        CityTo = x.RouteMap.CityTo
                    }
                }).AsQueryable();

            if (model == null)
            {
                return cargoes.ToList();
            }

            if (model.DateOf != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.StartDate >= model.DateOf);
            }
            if (model.DateTo != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.EndDate <= model.DateTo);
            }

            if (model.HeightOf != null)
            {
                cargoes = cargoes.Where(x => x.Height >= model.HeightOf);
            }
            if (model.HeightTo != null)
            {
                cargoes = cargoes.Where(x => x.Height <= model.HeightTo);
            }

            if (model.WidthOf != null)
            {
                cargoes = cargoes.Where(x => x.Width >= model.WidthOf);
            }
            if (model.WidthTo != null)
            {
                cargoes = cargoes.Where(x => x.Width <= model.WidthTo);
            }

            if (model.DepthOf != null)
            {
                cargoes = cargoes.Where(x => x.Depth >= model.DepthOf);
            }
            if (model.DepthTo != null)
            {
                cargoes = cargoes.Where(x => x.Depth <= model.DepthTo);
            }
            if (model.WeightOf != null)
            {
                cargoes = cargoes.Where(x => x.Weight >= model.WeightOf);
            }
            if (model.WeightTo != null)
            {
                cargoes = cargoes.Where(x => x.Weight <= model.WeightTo);

            }

            if (model.IdTypeCurrency != null)
            {
                cargoes = cargoes.Where(x => x.IdTypeCurrency == model.IdTypeCurrency);
            }
            if (model.IdTypePayment != null)
            {
                cargoes = cargoes.Where(x => x.IdTypePayment == model.IdTypePayment);
            }
            if (model.TypeCargo != null)
            {
                
                var tr = cargoes.Select(x => x.TypeCargo);
                var trans = cargoes;
                foreach (var t in model.TypeCargo)
                {
                    if (cargoes.Any(x => x.TypeCargo.Contains(t)))
                    {
                        trans = trans.Where(x => x.TypeCargo.Contains(t));
                    }
                }

                cargoes = trans;
            }

            if (model.CountryOf != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.CountyFrom.ToUpper().Contains(model.CountryOf.ToUpper()));
            }
            if (model.CountryTo != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.CountyTo.ToUpper().Contains(model.CountryTo.ToUpper()));
            }

            if (model.CityOf != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.CityFrom.ToUpper().Contains(model.CityOf.ToUpper()));
            }
            if (model.CityTo != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.CityTo.ToUpper().Contains(model.CityTo.ToUpper()));
            }

            if (model.StateOf != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.StateFrom.ToUpper().Contains(model.StateOf.ToUpper()));
            }

            if (model.StateTo != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.StateTo.ToUpper().Contains(model.StateTo.ToUpper()));
            }

            if (model.PostcodeOf != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.PostCodeFrom.ToUpper().Contains(model.PostcodeOf.ToUpper()));
            }

            if (model.PostcodeTo != null)
            {
                cargoes = cargoes.Where(x => x.RouteMap.PostCodeTo.ToUpper().Contains(model.PostcodeTo.ToUpper()));
            }
            return await cargoes.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Cargo> GetCargoId(int id)
        {
            return await _context.Cargoes.FindAsync(id);
        }
    }
}
