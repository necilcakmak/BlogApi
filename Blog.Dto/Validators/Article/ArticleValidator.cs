using Blog.Dto.Article;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.Article
{
    public class ArticleValidator : AbstractValidator<ArticleAddDto>
    {
        public ArticleValidator()
        {
            RuleFor(x => x.Title)
              .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");        
            RuleFor(x => x.Content)
              .Length(5, 5000).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.CategoryId)
              .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
