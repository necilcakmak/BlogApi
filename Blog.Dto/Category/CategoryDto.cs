using Blog.Dto.Article;
using Blog.Dto.MainCategory;
using Blog.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? TagName { get; set; }
        public Guid ParentCategoryId { get; set; }
        public ParentCategoryDto ParentCategoryDto { get; set; }
    }
}
