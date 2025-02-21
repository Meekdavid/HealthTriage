using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;
using Common.Enums;

namespace Persistence.DBModels
{
    public class MedicalActivityLog : HealthTriageEntity
    {
        public MedicalActivityLog()
        {
            Status = Status.Active;
        }

        public string MedicalActivityLogId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Details { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
