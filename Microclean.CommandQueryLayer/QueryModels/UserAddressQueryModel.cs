using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class UserAddressQueryModel
    {
        public long AddressId { get; set; }
        public string FullAddress { get; set; }
        public int UserZip { get; set; }
        public int StateId { get; set; }
        public long DistrictId { get; set; }
        public long CityLocationId { get; set; }
    }
}
