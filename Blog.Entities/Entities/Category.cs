using Blog.Entities.Entities.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Entities
{
    public class Category : EntityBase
    {
        public Guid MainCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? TagName { get; set; }
        public MainCategory MainCategory { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
