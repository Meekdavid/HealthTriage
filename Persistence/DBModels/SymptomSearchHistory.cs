using Persistence.Concrete;
using System.Collections.Generic;
using Persistence.Enums;
using Persistence.DBModels.JoinDBModels;

namespace Persistence.DBModels
{
    public class SymptomSearchHistory : HealthTriageEntity
    {
        public SymptomSearchHistory()
        {
            Status = Status.Active;
            SymptomSearchHistoryId = string.Empty;
            UserId = string.Empty;
            SymptomSearchHistorySymptoms = new HashSet<SymptomSearchHistorySymptom>();
            SymptomSearchHistoryTreatmentOptions = new HashSet<SymptomSearchHistoryTreatmentOption>();
        }

        public string SymptomSearchHistoryId { get; set; }
        public string UserId { get; set; }

        // Navigation properties
        public ICollection<SymptomSearchHistorySymptom> SymptomSearchHistorySymptoms { get; set; }
        public ICollection<SymptomSearchHistoryTreatmentOption> SymptomSearchHistoryTreatmentOptions { get; set; }
    }
}