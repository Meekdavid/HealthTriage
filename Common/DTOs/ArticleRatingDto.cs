using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ArticleRatingDto
    {
        public string ArticleRatingId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
    }
}
