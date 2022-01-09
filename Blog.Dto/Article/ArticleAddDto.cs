using Blog.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Article
{
    public class ArticleAddDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
