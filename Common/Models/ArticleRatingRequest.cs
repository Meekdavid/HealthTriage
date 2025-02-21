using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ArticleRatingRequest
    {
        public IFormFile CoverPhoto { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
    }
}
