using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;
using Persistence.DBModels.JoinDBModels;

namespace Persistence.DBModels
{
    public class ConsultationHistory : HealthTriageEntity
    {
        public ConsultationHistory()
        {
            Status = Status.Active;
            ConsultancyChats = new HashSet<ConsultancyChat>();
        }

        public string ConsultationHistoryId { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string PractitionerId { get; set; }
        public Practitioner Practitioner { get; set; }
        public ICollection<ConsultancyChat> ConsultancyChats { get; set; }
    }
}
