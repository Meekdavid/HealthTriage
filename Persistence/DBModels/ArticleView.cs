using Persistence.Concrete;
using Persistence.Enums;

public class ArticleView : HealthTriageEntity
{
    public ArticleView()
    {
        ArticleViewId = string.Empty;
        ArticleId = string.Empty;
        UserId = string.Empty;

        Status = Status.Active;
    }

    public string? ArticleViewId { get; set; }
    public string? ArticleId { get; set; }
    public string? UserId { get; set; } // Nullable for anonymous users

    // Navigation property
    public virtual Article? Article { get; set; }
}
