using Persistence.Concrete;
using Persistence.Enums;

public class ArticleRating : HealthTriageEntity
{
    public ArticleRating()
    {
        ArticleRatingId = string.Empty;
        ArticleId = string.Empty;
        UserId = string.Empty;
        Rating = default; // Defaults to 0

        Status = Status.Active;
    }

    public string? ArticleRatingId { get; set; }
    public string? ArticleId { get; set; }
    public string? UserId { get; set; }
    public int? Rating { get; set; } // Nullable to allow null values

    // Navigation property
    public virtual Article? Article { get; set; }
}
