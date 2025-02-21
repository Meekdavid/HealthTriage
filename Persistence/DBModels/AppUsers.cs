using Microsoft.AspNetCore.Identity;
using Persistence.Abstract;
using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBModels
{
    public class AppUser : IdentityUser<string>, IHealthTriageEntity
    {
        public AppUser()
        {
            EmailConfirmed = true;
            CreatedDate = DateTime.UtcNow;
        }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string EmergencyContact { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenEndDate { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
