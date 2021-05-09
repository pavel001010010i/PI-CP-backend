using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.Models.Models
{
    public class TransportModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public int IdTypeTransport { get; set; }
        public int IdTransLoadCapacity { get; set; }
        public int NumberAxes { get; set; }
        public int IdRouteMap { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int MaxLoadCapacity { get; set; }
        public double FuelConsumption { get; set; }
        public bool IsActive { get; set; }

    }
}
