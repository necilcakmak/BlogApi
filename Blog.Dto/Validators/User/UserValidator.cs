using Blog.Dto.User;
using FluentValidation;

namespace Blog.Dto.Validators.User
{
    public class UserValidator : AbstractValidator<UserUpdateDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.UserSurname)
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Password)
               .Equal(x => x.PasswordRepeat).WithMessage("{PropertyName} and PasswordRepeat must be equals")
               .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
               .NotNull().WithMessage("{PropertyName} must not be null")
               .When(x => x.PasswordIsChange == true);
            RuleFor(x => x.UserSurname)
               .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
               .NotNull().WithMessage("{PropertyName} must not be null");

        }
    }
}
