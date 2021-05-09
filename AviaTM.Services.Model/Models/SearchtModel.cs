using AviaTM.DB.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaTM.Services.Models.Models
{
    public class SearchtModel
    {
        public double? HeightOf { get; set; }
        public double? WidthOf { get; set; }
        public double? DepthOf { get; set; }
        public double? WeightOf { get; set; }
        public string CityOf { get; set; }
        public string StateOf { get; set; }
        public string PostcodeOf { get; set; }
        public string CountryOf { get; set; }
        public double? HeightTo { get; set; }
        public double? WidthTo{ get; set; }
        public double? DepthTo { get; set; }
        public double? WeightTo { get; set; }
        public string CityTo { get; set; }
        public string StateTo { get; set; }
        public string PostcodeTo { get; set; }
        public string CountryTo { get; set; }
        public DateTime? DateOf { get; set; }
        public DateTime? DateTo { get; set; }
        public int? IdTypeTransport { get; set; }
        public int? IdTypeCurrency { get; set; }
        public int? IdTypePayment { get; set; }
        public ICollection<TypeCargo> TypeCargo { get; set; }


    }
}
