using AviaTM;
using AviaTM.DB.IRepository;
using AviaTM.DB.Model.Models;
using AviaTM.Services.Models.Infastructure;
using AviaTM.Services.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTm.DB.Repository
{
    public class RouteMapRepository : IRouteMapRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContext _userContext;

        public RouteMapRepository(ApplicationDbContext context, UserContext userContext)
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
                Depth = model.Cargo.Depth,
                isStatus = model.Cargo.isStatus,
                Name = model.Cargo.Name,
                TypeCargo = model.Cargo.TypeCargo,
                IdRouteMap = RouteMap.Id               
            };

            _context.Cargoes.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Cargo> GetCargos()
        {
            var cargoes = _context.Cargoes.Where(x => x.IdUser.Contains(_userContext.UserId))
                .Select(x=> new Cargo
                {
                    Id = x.Id,
                    Name = x.Name,
                    Width = x.Width,
                    Height = x.Height,
                    Depth = x.Depth,
                    Weight = x.Weight,
                    isStatus = x.isStatus,
                    TypeCargo = x.TypeCargo,
                    IdRouteMap = x.IdRouteMap
                });
            return cargoes;
        }

        public async Task DeleteCargo(int id)
        {
            var cargo = await _context.Cargoes.FindAsync(id);

            _context.Cargoes.Remove(cargo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCargo(CargoModel model)
        {
            _context.TypeCargoes.AttachRange(model.TypeCargo);
            var cargo = _context.Cargoes.Include(x=>x.TypeCargo).FirstOrDefault(x=>x.Id == model.Id);
            _context.Cargoes.Attach(cargo);

            cargo.Name = model.Name;
            cargo.Height = model.Height;
            cargo.Width = model.Width;
            cargo.Depth = model.Depth;
            cargo.Weight = model.Weight;
            cargo.isStatus = model.isStatus;
            cargo.TypeCargo = model.TypeCargo;

            _context.Cargoes.Update(cargo); 
            
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RouteMap>> GetRouteMaps()
        {
            return await _context.RouteMaps.ToListAsync();
        }

        public Task UpdateRouteMap(RouteModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<RouteMap> GetRouteMap(int id)
        {
            return await _context.RouteMaps.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
