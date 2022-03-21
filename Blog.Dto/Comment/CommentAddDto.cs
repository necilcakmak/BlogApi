using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Comment
{
    public class CommentAddDto
    {
        public Guid ArticleId { get; set; }
        public string? Text { get; set; }
    }
}
