using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cook_Eat.models
{
    public class Address
    {
        public int StreetNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string NearestMetroStation { get; set; }
        public int UserId { get; set; }
    }
}
