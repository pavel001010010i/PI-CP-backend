using AviaTM.DB.Model.Models;
using AviaTM.DB.RepositoryService.IRepositoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.DB.RepositoryServer.RepositoryService
{
    public class TypeCargoService : ITypeCargoService
    {
        private readonly ApplicationDbContext _context;
        public TypeCargoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TypeCargo>> GetTypeCagroes()
        {
            return  _context.TypeCargoes.ToList();
        }
    }
}
