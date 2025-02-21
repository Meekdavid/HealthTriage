using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class FAQ : HealthTriageEntity
    {
        public FAQ()
        {
            Status = Status.Active;
        }
        public int FAQId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string AddedBy { get; set; }
    }
}
