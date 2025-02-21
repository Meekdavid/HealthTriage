using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SymptomSearchHistoryTreatmentOptions = new HashSet<SymptomSearchHistoryTreatmentOption>();
        }

        public string TreatmentOptionId { get; set; }
        public TreatmentType TreatmentType { get; set; }
        public SeverityLevel SeverityLevel { get; set; }
        public string Details { get; set; }
        public string Name { get; set; }

        public ICollection<SymptomSearchHistoryTreatmentOption> SymptomSearchHistoryTreatmentOptions { get; set; }
    }
}
