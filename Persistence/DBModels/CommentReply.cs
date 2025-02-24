using Persistence.Concrete;
using Persistence.Enums;

public class CommentReply : HealthTriageEntity
{
    public CommentReply()
    {
        CommentReplyId = string.Empty;
        CommentId = string.Empty;
        UserId = string.Empty;
        ReplyText = string.Empty;

        Status = Status.Active;
    }

    public string? CommentReplyId { get; set; }
    public string? CommentId { get; set; }
    public string? UserId { get; set; }
    public string? ReplyText { get; set; }

    public virtual AppUser? User { get; set; }
    public virtual ArticleComment? ArticleComment { get; set; }
}
