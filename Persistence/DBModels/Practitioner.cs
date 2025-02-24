using Persistence.Concrete;
using System;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class Practitioner : HealthTriageEntity
    {
        public Practitioner()
        {
            Status = Status.Passive;
            PractitionerId = string.Empty;
            PractitionerTitle = string.Empty;
            PractitionerName = string.Empty;
            MedicalLicenseNumber = string.Empty;
            Experience = string.Empty;
            Institution = string.Empty;
            WorkAddress = string.Empty;
            WorkEmail = string.Empty;
            GovernmentId = string.Empty;
            ApplicationCertificateUrl = string.Empty;
            Rating = null; // Nullable
        }

        public string PractitionerId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string PractitionerTitle { get; set; }
        public string PractitionerName { get; set; }
        public string MedicalLicenseNumber { get; set; }
        public string Experience { get; set; }
        public string Institution { get; set; }
        public string WorkAddress { get; set; }
        public string WorkEmail { get; set; }
        public string GovernmentId { get; set; }
        public string ApplicationCertificateUrl { get; set; }
        public int? Rating { get; set; } // Nullable for flexibility
    }
}
