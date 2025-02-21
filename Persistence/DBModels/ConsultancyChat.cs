using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class ConsultancyChat : HealthTriageEntity
    {
        public ConsultancyChat()
        {
            Status = Status.Active;
        }

        public string ConsultancyChatId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
    }
}
