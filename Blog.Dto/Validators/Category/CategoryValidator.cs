using Blog.Dto.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.Category
{
    public class CategoryValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
              .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
