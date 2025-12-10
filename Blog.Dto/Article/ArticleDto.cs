using Blog.Dto.Category;
using Blog.Dto.Comment;
using Blog.Dto.User;
using Blog.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Article
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Thumbnail { get; set; }
        public string? ImageUrl { get; set; }
        public string? Slug { get; set; }
        public string? Keywords { get; set; }
        public int ViewsCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public DateTime PublishedDate { get; set; }
        public UserDto User { get; set; }
        public CategoryDto Category { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
