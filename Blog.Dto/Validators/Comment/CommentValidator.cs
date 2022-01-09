using Blog.Dto.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.Comment
{
    public class CommentValidator : AbstractValidator<CommentAddDto>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Text)
              .Length(5, 500).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.ArticleId)
              .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
