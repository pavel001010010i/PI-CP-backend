using System;
using System.Collections.Generic;
using System.Text;

namespace AviaTM.DB.Model.Models
{
    public class RouteMap
    {
        public int Id { get; set; }
        public string CountryCodeFrom { get; set; }
        public float latFrom { get; set; }
        public float lngFrom { get; set; }
        public string CountyFrom { get; set; }
        public string CityFrom { get; set; }
        public string StateFrom { get; set; }
        public string StreetFrom { get; set; }
        public string PostCodeFrom { get; set; }
        public string FullAddressFrom { get; set; }
        public string CountryCodeTo { get; set; }
        public float latTo{ get; set; }
        public float lngTo{ get; set; }
        public string CountyTo { get; set; }
        public string CityTo { get; set; }
        public string StateTo { get; set; }
        public string StreetTo { get; set; }
        public string PostCodeTo { get; set; }
        public string FullAddressTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public virtual ICollection<Transport> Transports { get; set; }
        public virtual ICollection<Cargo> Cargos { get; set; }

    }
}
