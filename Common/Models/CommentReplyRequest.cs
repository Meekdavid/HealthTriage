using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CommentReplyRequest
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string ReplyText { get; set; }
    }
}
