using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ConsultationHistoryResponseDto
    {
        public string ConsultationHistoryId { get; set; }
        public string UserId { get; set; }
        public string PatientName { get; set; } // Optional, if needed
        public string PractitionerId { get; set; }
        public string PractitionerName { get; set; } // Optional, if needed
        public List<ConsultancyChatResponseDto> ConsultancyChats { get; set; }
    }

}
