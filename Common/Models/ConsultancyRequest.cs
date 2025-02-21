using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ConsultancyRequest
    {
        public string UserId { get; set; }
        public string PractitionerId { get; set; }
        public string UserRequest { get; set; }
    }

    public class PractitionerReplyToConsultancy
    {
        public string PractitionerId { get; set; }
        public string ConsultationId { get; set; }
        public string ReplyText { get; set; }
    }
}
