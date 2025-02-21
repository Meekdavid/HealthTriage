using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class PractitionerRequest
    {
        public string MedicalLicenseNumber { get; set; }
        public string Experience { get; set; }
        public string Institution { get; set; }
        public string WorkAdress { get; set; }
        public string WorkEmail { get; set; }
        public string GovernmentId { get; set; }
    }
}
