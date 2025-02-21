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
        public int RefreshTokenExpiryDays { get; set; }
    }
}
