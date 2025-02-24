using Persistence.Concrete;
using System.Collections.Generic;
using Persistence.Enums;
using Common.Enums;
using Persistence.DBModels.JoinDBModels;

namespace Persistence.DBModels
{
    public class TreatmentOption : HealthTriageEntity
    {
        public TreatmentOption()
        {
            Status = Status.Active;
            TreatmentOptionId = string.Empty;
            Details = string.Empty;
            Name = string.Empty;
            SymptomSearchHistoryTreatmentOptions = new HashSet<SymptomSearchHistoryTreatmentOption>();
        }

        public string TreatmentOptionId { get; set; }
        public TreatmentType TreatmentType { get; set; }
        public SeverityLevel SeverityLevel { get; set; }
        public string Details { get; set; }
        public string Name { get; set; }

        // Navigation property
        public ICollection<SymptomSearchHistoryTreatmentOption> SymptomSearchHistoryTreatmentOptions { get; set; }
    }
}