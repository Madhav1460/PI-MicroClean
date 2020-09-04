using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class AddressRequestModel
    {
        public int AddressId { get; set; }
        public string FullAdrrss { get; set; }
        public string LandMark { get; set; }
        public long CityLocationId { get; set; }
        public int ZipCode { get; set; }

    }
}
