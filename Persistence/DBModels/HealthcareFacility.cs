using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class HealthcareFacility : HealthTriageEntity
    {
        public HealthcareFacility()
        {
            Status = Status.Active;
        }
        public string HealthcareFacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityType { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public string OperatingHours { get; set; }
        public string ServicesOffered { get; set; }
        public string Specialties { get; set; }
        public string LicenseNumber { get; set; }
        public string AccreditationStatus { get; set; }
        public float FacilityRating { get; set; }
        public bool EmergencyServicesAvailable { get; set; }
        public string InsuranceAccepted { get; set; }
        public int? NumberOfBeds { get; set; }  // Nullable for clinics or pharmacies
        public int StaffCount { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}
