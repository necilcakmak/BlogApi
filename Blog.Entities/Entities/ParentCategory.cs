using Blog.Entities.Entities.Base;

namespace Blog.Entities.Entities
{
    public class ParentCategory : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
