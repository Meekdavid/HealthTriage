using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class MedicalActivityLogResponseDto
    {
        public string MedicalActivityLogId { get; set; }
        public string UserId { get; set; }
        public string Details { get; set; }
        public string ActivityType { get; set; } // Enum will be returned as a string
        public DateTime TimeOfAction { get; set; }
    }

}
