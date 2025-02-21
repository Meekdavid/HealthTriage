using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CommentReplyDto
    {
        public DateTime CommentDate { get; set; }
        public string CommentReplyId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ReplyText { get; set; }
    }
}
