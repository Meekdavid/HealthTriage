using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserRegisterRequest
    {
        public string FullName { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string EmergencyContact { get; set; }
    }
}
