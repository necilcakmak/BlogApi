using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Article
{
    public class ArticleUpdateDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid CategoryId { get; set; }
        public string? Thumbnail { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
    }
}
