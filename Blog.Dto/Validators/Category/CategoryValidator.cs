﻿using Blog.Dto.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.Category
{
    public class CategoryAddValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddValidator()
        {
            RuleFor(x => x.Name)
              .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Name)
              .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Id)
             .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.ParentCategoryId)
             .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
