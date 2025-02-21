using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DBModels.JoinDBModels
{
    public class SymptomSearchHistorySymptom
    {
        public string SymptomSearchHistoryId { get; set; }
        public SymptomSearchHistory SymptomSearchHistory { get; set; }

        public string SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
