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
    public class ArticleRating : HealthTriageEntity
    {
        public ArticleRating()
        {
            Status = Status.Active;
        }
        public string ArticleRatingId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; } // Rating between 1 and 5
        // Navigation property
        public virtual Article Article { get; set; }
    }
}
