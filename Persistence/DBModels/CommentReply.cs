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
    public class CommentReply : HealthTriageEntity
    {
        public CommentReply()
        {
            Status = Status.Active;
        }
        public string CommentReplyId { get; set; }
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string ReplyText { get; set; }

        // Navigation property
        public virtual ArticleComment ArticleComment { get; set; }
    }
}
