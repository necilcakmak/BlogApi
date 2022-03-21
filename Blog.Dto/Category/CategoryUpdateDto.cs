using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Category
{
    public class CategoryUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? TagName { get; set; }
        public Guid ParentCategoryId { get; set; }
    }
}
