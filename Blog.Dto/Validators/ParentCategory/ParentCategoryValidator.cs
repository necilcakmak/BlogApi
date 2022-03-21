using Blog.Dto.ParentCategory;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.ParentCategory
{
    public class ParentCategoryAddValidator : AbstractValidator<ParentCategoryAddDto>
    {
        public ParentCategoryAddValidator()
        {
            RuleFor(x => x.Name)
              .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }

    public class ParentCategoryUpdateValidator : AbstractValidator<ParentCategoryUpdateDto>
    {
        public ParentCategoryUpdateValidator()
        {
            RuleFor(x => x.Name)
              .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Id)
             .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
