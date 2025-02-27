using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class HospitalClientResponse
    {
        public float version { get; set; }
        public string generator { get; set; }
        public Osm3s osm3s { get; set; }
        public Element[] elements { get; set; }
    }

    public class Osm3s
    {
        public DateTime timestamp_osm_base { get; set; }
        public DateTime timestamp_areas_base { get; set; }
        public string copyright { get; set; }
    }

    public class Element
    {
        public string type { get; set; }
        public long id { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public HospitalClientResponseDto tags { get; set; }
        public Center center { get; set; }
        public long[] nodes { get; set; }
    }

    //public class HospitalClientResponseDto
    //{
    //    public string amenity { get; set; }
    //    public string fixme { get; set; }
    //    public string name { get; set; }
    //    public string addrstreet { get; set; }
    //    public string addrcity { get; set; }
    //    public string addrhousenumber { get; set; }
    //    public string addrpostcode { get; set; }
    //    public string healthcare { get; set; }
    //    public string healthcarespeciality { get; set; }
    //    public string namepcm { get; set; }
    //    public string _operator { get; set; }
    //    public string operatortype { get; set; }
    //    public string mobile { get; set; }
    //    public string contactfacebook { get; set; }
    //    public string description { get; set; }
    //    public string email { get; set; }
    //    public string emergency { get; set; }
    //    public string opening_hours { get; set; }
    //    public string paymentcash { get; set; }
    //    public string paymentdebit_cards { get; set; }
    //    public string phone { get; set; }
    //    public string source { get; set; }
    //    public string website { get; set; }
    //    public string facility_manager { get; set; }
    //    public string sourceposition { get; set; }
    //    public string status { get; set; }
    //    public string users_PLWD { get; set; }
    //    public string users_boy { get; set; }
    //    public string users_elderly { get; set; }
    //    public string users_female { get; set; }
    //    public string users_girl { get; set; }
    //    public string users_men { get; set; }
    //    public string facility_use { get; set; }
    //    public string building { get; set; }
    //    public string check_date { get; set; }
    //    public string contactphone { get; set; }
    //    public string contactwebsite { get; set; }
    //    public string wikidata { get; set; }
    //    public string wikipedia { get; set; }
    //    public string short_name { get; set; }
    //    public string height { get; set; }
    //    public string start_date { get; set; }
    //    public string surface { get; set; }
    //    public string alt_name { get; set; }
    //    public string health_facilitytype { get; set; }
    //    public string level { get; set; }
    //}

    public class HospitalClientResponseDto
    {
        private string DefaultValue(string value) => value ?? "Unknown";

        private string _amenity = "Unknown";
        private string _fixme = "Unknown";
        private string _name = "Unknown";
        private string _addrstreet = "Unknown";
        private string _addrcity = "Unknown";
        private string _addrhousenumber = "Unknown";
        private string _addrpostcode = "Unknown";
        private string _healthcare = "Unknown";
        private string _healthcarespeciality = "Unknown";
        private string _namepcm = "Unknown";
        private string __operator = "Unknown";
        private string _operatortype = "Unknown";
        private string _mobile = "Unknown";
        private string _contactfacebook = "Unknown";
        private string _description = "Unknown";
        private string _email = "Unknown";
        private string _emergency = "Unknown";
        private string _opening_hours = "Unknown";
        private string _paymentcash = "Unknown";
        private string _paymentdebit_cards = "Unknown";
        private string _phone = "Unknown";
        private string _source = "Unknown";
        private string _website = "Unknown";
        private string _facility_manager = "Unknown";
        private string _sourceposition = "Unknown";
        private string _status = "Unknown";
        private string _users_PLWD = "Unknown";
        private string _users_boy = "Unknown";
        private string _users_elderly = "Unknown";
        private string _users_female = "Unknown";
        private string _users_girl = "Unknown";
        private string _users_men = "Unknown";
        private string _facility_use = "Unknown";
        private string _building = "Unknown";
        private string _check_date = "Unknown";
        private string _contactphone = "Unknown";
        private string _contactwebsite = "Unknown";
        private string _wikidata = "Unknown";
        private string _wikipedia = "Unknown";
        private string _short_name = "Unknown";
        private string _height = "Unknown";
        private string _start_date = "Unknown";
        private string _surface = "Unknown";
        private string _alt_name = "Unknown";
        private string _health_facilitytype = "Unknown";
        private string _level = "Unknown";

        public string amenity { get => _amenity; set => _amenity = DefaultValue(value); }
        public string fixme { get => _fixme; set => _fixme = DefaultValue(value); }
        public string name { get => _name; set => _name = DefaultValue(value); }
        public string addrstreet { get => _addrstreet; set => _addrstreet = DefaultValue(value); }
        public string addrcity { get => _addrcity; set => _addrcity = DefaultValue(value); }
        public string addrhousenumber { get => _addrhousenumber; set => _addrhousenumber = DefaultValue(value); }
        public string addrpostcode { get => _addrpostcode; set => _addrpostcode = DefaultValue(value); }
        public string healthcare { get => _healthcare; set => _healthcare = DefaultValue(value); }
        public string healthcarespeciality { get => _healthcarespeciality; set => _healthcarespeciality = DefaultValue(value); }
        public string namepcm { get => _namepcm; set => _namepcm = DefaultValue(value); }
        public string _operator { get => __operator; set => __operator = DefaultValue(value); }
        public string operatortype { get => _operatortype; set => _operatortype = DefaultValue(value); }
        public string mobile { get => _mobile; set => _mobile = DefaultValue(value); }
        public string contactfacebook { get => _contactfacebook; set => _contactfacebook = DefaultValue(value); }
        public string description { get => _description; set => _description = DefaultValue(value); }
        public string email { get => _email; set => _email = DefaultValue(value); }
        public string emergency { get => _emergency; set => _emergency = DefaultValue(value); }
        public string opening_hours { get => _opening_hours; set => _opening_hours = DefaultValue(value); }
        public string paymentcash { get => _paymentcash; set => _paymentcash = DefaultValue(value); }
        public string paymentdebit_cards { get => _paymentdebit_cards; set => _paymentdebit_cards = DefaultValue(value); }
        public string phone { get => _phone; set => _phone = DefaultValue(value); }
        public string source { get => _source; set => _source = DefaultValue(value); }
        public string website { get => _website; set => _website = DefaultValue(value); }
        public string facility_manager { get => _facility_manager; set => _facility_manager = DefaultValue(value); }
        public string sourceposition { get => _sourceposition; set => _sourceposition = DefaultValue(value); }
        public string status { get => _status; set => _status = DefaultValue(value); }
        public string users_PLWD { get => _users_PLWD; set => _users_PLWD = DefaultValue(value); }
        public string users_boy { get => _users_boy; set => _users_boy = DefaultValue(value); }
        public string users_elderly { get => _users_elderly; set => _users_elderly = DefaultValue(value); }
        public string users_female { get => _users_female; set => _users_female = DefaultValue(value); }
        public string users_girl { get => _users_girl; set => _users_girl = DefaultValue(value); }
        public string users_men { get => _users_men; set => _users_men = DefaultValue(value); }
        public string facility_use { get => _facility_use; set => _facility_use = DefaultValue(value); }
        public string building { get => _building; set => _building = DefaultValue(value); }
        public string check_date { get => _check_date; set => _check_date = DefaultValue(value); }
        public string contactphone { get => _contactphone; set => _contactphone = DefaultValue(value); }
        public string contactwebsite { get => _contactwebsite; set => _contactwebsite = DefaultValue(value); }
        public string wikidata { get => _wikidata; set => _wikidata = DefaultValue(value); }
        public string wikipedia { get => _wikipedia; set => _wikipedia = DefaultValue(value); }
        public string short_name { get => _short_name; set => _short_name = DefaultValue(value); }
        public string height { get => _height; set => _height = DefaultValue(value); }
        public string start_date { get => _start_date; set => _start_date = DefaultValue(value); }
        public string surface { get => _surface; set => _surface = DefaultValue(value); }
        public string alt_name { get => _alt_name; set => _alt_name = DefaultValue(value); }
        public string health_facilitytype { get => _health_facilitytype; set => _health_facilitytype = DefaultValue(value); }
        public string level { get => _level; set => _level = DefaultValue(value); }
    }


    public class Center
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }

}
