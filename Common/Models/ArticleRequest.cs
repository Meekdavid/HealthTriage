using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ArticleRequest
    {
        public string? UserId { get; set; }
        public IFormFile? CoverPhoto { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ArticleCategory Category { get; set; }
        public AuthorType AuthorType { get; set; } // "User" or "Practitioner"
        public ArticleStatus ArticleState { get; set; } // "Published", "Draft", "Pending Review"
    }
}
