using Persistence.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class Admin : HealthTriageEntity
    {
        public Admin()
        {
            Status = Status.Active;
        }

        public string AdminId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
