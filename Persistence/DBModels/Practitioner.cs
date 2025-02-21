using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class Practitioner : HealthTriageEntity
    {
        public Practitioner()
        {
            Status = Status.Active;
        }
        public string PractitionerId { get; set; }
        public string PractitionerTitle { get; set; }
        public string PractitionerName { get; set; }
        public string MedicalLicenseNumber { get; set; }
        public string Experience { get; set; }
        public string Institution { get; set; }
        public string WorkAddress { get; set; }
        public string WorkEmail { get; set; }
        public string GovernmentId { get; set; }
        public string ApplicationCertificateUrl { get; set; }
        public int? rating { get; set; }
    }
}
