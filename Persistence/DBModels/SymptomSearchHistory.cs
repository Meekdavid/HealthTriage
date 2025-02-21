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
    public class SymptomSearchHistory : HealthTriageEntity
    {
        public SymptomSearchHistory()
        {
            Status = Status.Active;
            SymptomSearchHistorySymptoms = new HashSet<SymptomSearchHistorySymptom>();
            SymptomSearchHistoryTreatmentOptions = new HashSet<SymptomSearchHistoryTreatmentOption>();
        }
        public string SymptomSearchHistoryId { get; set; }
        public string UserId { get; set; }

        public ICollection<SymptomSearchHistorySymptom> SymptomSearchHistorySymptoms { get; set; }
        public ICollection<SymptomSearchHistoryTreatmentOption> SymptomSearchHistoryTreatmentOptions { get; set; }
    }
}
