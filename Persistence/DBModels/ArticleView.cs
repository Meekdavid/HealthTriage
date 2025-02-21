using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Concrete;
using Persistence.Enums;

namespace Persistence.DBModels
{
    public class ArticleView : HealthTriageEntity
    {
        public ArticleView()
        {
            Status = Status.Active;
        }
        public string ArticleViewId { get; set; }
        public string ArticleId { get; set; }
        public string? UserId { get; set; } // Nullable for anonymous users
        // Navigation property
        public virtual Article Article { get; set; }
    }
}
