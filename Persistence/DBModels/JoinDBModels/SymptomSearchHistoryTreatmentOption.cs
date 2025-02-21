using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBModels.JoinDBModels
{
    public class SymptomSearchHistoryTreatmentOption
    {
        public string SymptomSearchHistoryId { get; set; }
        public SymptomSearchHistory SymptomSearchHistory { get; set; }

        public string TreatmentOptionId { get; set; }
        public TreatmentOption TreatmentOption { get; set; }
    }
}
