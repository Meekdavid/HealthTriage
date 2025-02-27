using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CitiesOfSpecifiedCountry
    {
        public string country { get; set; }
    }

    public class CitiesOfSpecifiedCountryResponse
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public List<string> data { get; set; }
    }

    public class CitiesOfSpecifiedState
    {
        public string country { get; set; }
        public string state { get; set; }
    }

    public class CitiesOfSpecifiedStateResponse
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public List<string> data { get; set; }
    }


    public class StatesOfSpecifiedCountry
    {
        public string country { get; set; }
    }


    public class StatesOfSpecifiedCountryResponse
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string name { get; set; }
        public string iso3 { get; set; }
        public List<State> states { get; set; }
    }

    public class State
    {
        public string name { get; set; }
        public string state_code { get; set; }
    }

}
