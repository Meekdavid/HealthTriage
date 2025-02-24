using Persistence.Concrete;
using Persistence.DBModels;
using Persistence.Enums;

public class ArticleComment : HealthTriageEntity
{
    public ArticleComment()
    {
        ArticleCommentId = string.Empty;
        ArticleId = string.Empty;
        UserId = string.Empty;
        CommentText = string.Empty;

        Status = Status.Active;
        CommentReplies = new HashSet<CommentReply>();
    }

    public string? ArticleCommentId { get; set; }
    public string? ArticleId { get; set; }
    public string? UserId { get; set; }
    public string? CommentText { get; set; }

    // Navigation properties
    public virtual AppUser? User { get; set; }
    public virtual Article? Article { get; set; }
    public virtual ICollection<CommentReply> CommentReplies { get; set; }
}
