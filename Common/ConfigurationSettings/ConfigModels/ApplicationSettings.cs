using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConfigurationSettings.ConfigModels
{
    public class ApplicationSettings
    {
        public EmailDetails EmailDetails { get; set; }
        public FireBaseStorage FireBaseStorage { get; set; }
        public string JwtSecret { get; set; }
        public string BaseLocalStorageDomain { get; set; }
        public string HealthTriageHomePage { get; set; }
        public string HealthTriageUnsubscribeLink { get; set; }
        public string MedicalFacilityEndpoint { get; set; }
        public string CitiesofSpecifiedCountry { get; set; }
        public string CitiesofSpecifiedState { get; set; }
        public string StatesofSpecifiedCountry { get; set; }
        public int RefreshTokenExpiryDays { get; set; }
    }
}
