using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class CityLocationQueryMode
    {
        public long Id { get; set; }
        public string CityLocation { get; set; }
        public string ExtendedCityLocation { get; set; }
        public int? Pincode { get; set; }
    }
}
