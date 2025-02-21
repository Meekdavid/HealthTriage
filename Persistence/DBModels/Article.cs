using Common.Enums;
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
    public class Article : HealthTriageEntity
    {
        public Article()
        {
            Status = Status.Active;
            ArticleViews = new HashSet<ArticleView>();
            ArticleRatings = new HashSet<ArticleRating>();
            ArticleComments = new HashSet<ArticleComment>();
        }
        public string ArticleId { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string? UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ArticleCategory Category { get; set; }
        public AuthorType AuthorType { get; set; } // "User" or "Practitioner"
        public ArticleStatus ArticleState { get; set; } // "Published", "Draft", "Pending Review"
        // Navigation properties
        public virtual ICollection<ArticleView> ArticleViews { get; set; }
        public virtual ICollection<ArticleRating> ArticleRatings { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
    }
}
