using Persistence.Concrete;
using System;
using Persistence.Enums;
using Common.Enums;

namespace Persistence.DBModels
{
    public class MedicalActivityLog : HealthTriageEntity
    {
        public MedicalActivityLog()
        {
            Status = Status.Active;
            MedicalActivityLogId = string.Empty;
            UserId = string.Empty;
            Details = string.Empty;
            ActivityType = default;
        }

        public string MedicalActivityLogId { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public string Details { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
