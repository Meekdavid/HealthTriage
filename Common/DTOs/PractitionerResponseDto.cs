using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PractitionerResponseDto
    {
        public string PractitionerId { get; set; }
        public string PractitionerTitle { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string UserName { get; set; }
        public string PractitionerName { get; set; }
        public string MedicalLicenseNumber { get; set; }
        public string Experience { get; set; }
        public string Institution { get; set; }
        public string WorkAddress { get; set; } // Fixed typo from 'WorkAdress' to 'WorkAddress'
        public string WorkEmail { get; set; }
        public string GovernmentId { get; set; }
        public string ApplicationCertificateUrl { get; set; }
        public double? Rating { get; set; }
        public int? TotalRating { get; set; }
    }

}
