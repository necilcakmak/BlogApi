using Blog.Entities.Entities.Base;

namespace Blog.Entities.Entities
{
    public class MainCategory : EntityBase
    {
        public string MainCategoryName { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
