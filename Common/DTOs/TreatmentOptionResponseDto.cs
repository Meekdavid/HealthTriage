using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class TreatmentOptionResponseDto
    {
        public string TreatmentOptionId { get; set; }
        public string TreatmentType { get; set; } // TreatmentType as a string
        public string SeverityLevel { get; set; } // SeverityLevel as a string
        public string Details { get; set; }
        public string Name { get; set; }
    }

}
