using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ArticleCommentDto
    {
        public DateTime CommentDate { get; set; }
        public string ArticleCommentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CommentText { get; set; }
        public List<CommentReplyDto> Replies { get; set; }
    }
}
