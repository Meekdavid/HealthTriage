using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ArticleResponseDto
    {
        public string ArticleId { get; set; }
        public string? UserId { get; set; }
        public string Title { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string AuthorType { get; set; }
        public string ArticleState { get; set; }
        public int ViewCount { get; set; }
        public double AverageRating { get; set; }
        public int TotalComments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public List<ArticleCommentDto> Comments { get; set; }
        public List<ArticleRatingDto> Ratings { get; set; }
        public List<ArticleViewDto> Views { get; set; }
    }

}
