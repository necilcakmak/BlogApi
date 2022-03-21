using Blog.Entities.Entities.Base;

namespace Blog.Entities.Entities
{
    public class Category : EntityBase
    {  
        public string? Name { get; set; }
        public string? TagName { get; set; }
        public Guid ParentCategoryId { get; set; }
        public ParentCategory ParentCategory { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
