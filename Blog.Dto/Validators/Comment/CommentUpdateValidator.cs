using Blog.Dto.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.Comment
{
    public class CommentUpdateValidator : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateValidator()
        {
            RuleFor(x => x.Text)
              .Length(5, 500).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
              .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Id)
              .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
