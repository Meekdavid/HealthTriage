using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBModels
{
    public class Country
    {
        public string CountryName { get; set; }
        public string ISOCode2 { get; set; }
        public string ISOCode3 { get; set; }
        public string PhoneCode { get; set; }
        public string Flag { get; set; }
    }
}
