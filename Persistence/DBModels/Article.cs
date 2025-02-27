using Common.Enums;
using Persistence.Concrete;
using Persistence.DBModels;

public class Article : HealthTriageEntity
{
    public Article()
    {
        ArticleId = string.Empty;
        CoverPhotoUrl = string.Empty;
        UserId = string.Empty;
        Title = string.Empty;
        Content = string.Empty;
        Category = default;
        AuthorType = default;
        ArticleState = ArticleStatus.PendingReview;
        Status = Persistence.Enums.Status.Active;

        ArticleViews = new HashSet<ArticleView>();
        ArticleRatings = new HashSet<ArticleRating>();
        ArticleComments = new HashSet<ArticleComment>();
    }

    public string? ArticleId { get; set; }
    public string? CoverPhotoUrl { get; set; }
    public string? UserId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? ApprovedBy { get; set; }
    public string?  { get; set; }
    public string? ApprovedBy { get; set; }
    public ArticleCategory? Category { get; set; }
    public AuthorType? AuthorType { get; set; }
    public ArticleStatus? ArticleState { get; set; }

    // Navigation properties
    public virtual ICollection<ArticleView> ArticleViews { get; set; }
    public virtual ICollection<ArticleRating> ArticleRatings { get; set; }
    public virtual ICollection<ArticleComment> ArticleComments { get; set; }
}
