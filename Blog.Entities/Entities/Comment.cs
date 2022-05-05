using Blog.Entities.Entities.Base;

namespace Blog.Entities.Entities
{
    public class Comment : EntityBase
    {
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
        public string? Text { get; set; }
        public virtual Article? Article { get; set; }
        public virtual User? User { get; set; }
    }
}
