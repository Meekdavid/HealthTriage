using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Concrete;
using Persistence.Enums;
using Common.Enums;
using Persistence.DBModels.JoinDBModels;

namespace Persistence.DBModels
{
    public class ArticleComment : HealthTriageEntity
    {
        public ArticleComment()
        {
            Status = Status.Active;
            CommentReplies = new HashSet<CommentReply>();
        }
        public string ArticleCommentId { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string CommentText { get; set; }

        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual ICollection<CommentReply> CommentReplies { get; set; }
    }
}
