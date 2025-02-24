using Persistence.Concrete;
using System.Collections.Generic;
using Persistence.Enums;
using Persistence.DBModels.JoinDBModels;

namespace Persistence.DBModels
{
    public class Symptom : HealthTriageEntity
    {
        public Symptom()
        {
            Status = Status.Active;
            SymptomId = string.Empty;
            Title = string.Empty;
            SymptomSearchHistorySymptoms = new HashSet<SymptomSearchHistorySymptom>();
        }

        public string SymptomId { get; set; }
        public string Title { get; set; }

        // Navigation property
        public ICollection<SymptomSearchHistorySymptom> SymptomSearchHistorySymptoms { get; set; }
    }
}
