using AviaTM;
using AviaTM.DB.IRepository;
using AviaTM.DB.Model.Models;
using AviaTM.Services.IServicesController;
using AviaTM.Services.Models.Infastructure;
using AviaTM.Services.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTm.Services.Api.ServicesController
{
    public class CargoControllerService : ICargoControllerService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserContext _userContext;
        private ICargoRepository _repository;

        public CargoControllerService(ApplicationDbContext context, UserContext userContext, ICargoRepository repository)
        {
            _context = context;
            _userContext = userContext;
            _repository = repository;
        }
        public async Task AddCargos(RouteModel model)
        {
            await _repository.AddCargos(model);
            
        }

        public IEnumerable<Cargo> GetCargos()
        {
            return _repository.GetCargos();
        }
        public IEnumerable<Cargo> GetCargoesForSelectRequest()
        {
            return _repository.GetCargoesForSelectRequest();
        }

        public async Task DeleteCargo(int id)
        {
            await _repository.DeleteCargo(id);
        }
        public async Task UpdateCargo(RouteModel model)
        {
           await _repository.UpdateCargo(model);
        }

        public async Task<IEnumerable<Cargo>> SearchCargo(SearchtModel model)
        {
            return await _repository.SearchCargo(model);
        }

        public async Task<Cargo> GetCargoId(int id)
        {
            return await _repository.GetCargoId(id);
        }
    }
}
