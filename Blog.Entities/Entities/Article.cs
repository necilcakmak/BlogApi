using Blog.Entities.Entities.Base;

namespace Blog.Entities.Entities
{
    public class Article : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Thumbnail { get; set; }
        public int ViewsCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public DateTime PublishedDate { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
