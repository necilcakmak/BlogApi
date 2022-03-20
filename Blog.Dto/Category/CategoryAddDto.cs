using Blog.Dto.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Category
{
    public class CategoryAddDto
    {
        public string? Name { get; set; }
        public Guid parentCategoryId { get; set; }
    }
}
