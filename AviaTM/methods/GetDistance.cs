using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AviaTM.methods
{
    public class GetDistance
    {
        public double PI = Math.PI;
        public double earthRadiusKm =6371.0;
        public double deg2rad(double deg)
        {
            return (deg * PI / 180);
        }

        public double rad2deg(double rad)
        {
            return (rad * 180 / PI);
        }
        public double DistanceEarth(double lat1d, double lon1d, double lat2d, double lon2d)
        {
            double lat1r, lon1r, lat2r, lon2r, u, v;
            lat1r = deg2rad(lat1d);
            lon1r = deg2rad(lon1d);
            lat2r = deg2rad(lat2d);
            lon2r = deg2rad(lon2d);
            u = Math.Sin((lat2r - lat1r) / 2);
            v = Math.Sin((lon2r - lon1r) / 2);
            return 2.0 * earthRadiusKm * Math.Asin(Math.Sqrt(u * u + Math.Cos(lat1r) * Math.Cos(lat2r) * v * v));
        }
    }

}
