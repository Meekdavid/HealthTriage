using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class SymptomSearchHistoryResponseDto
    {
        public string SymptomSearchHistoryId { get; set; }
        public string UserId { get; set; }
        public List<string> Symptoms { get; set; } // List of symptom IDs for easy access
        public List<string> TreatmentOptions { get; set; } // List of treatment option IDs for easy access
    }

}
