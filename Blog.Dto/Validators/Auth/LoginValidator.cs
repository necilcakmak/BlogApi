using Blog.Dto.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
             .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
             .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Password)
            .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
            .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
