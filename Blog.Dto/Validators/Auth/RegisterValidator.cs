using Blog.Dto.Auth;
using FluentValidation;

namespace Blog.Dto.Validators.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("{PropertyName} is not valid")
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.NickName)
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.UserName)
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.UserSurname)
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.Password)
                .Length(5, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters")
                .NotNull().WithMessage("{PropertyName} must not be null");
            RuleFor(x => x.BirthDate)
               .Must(BeAValidAge).WithMessage("Age must be between 18 and 65")
               .NotNull().WithMessage("{PropertyName} must not be null");

        }
        private bool BeAValidAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            var age = (a - b) / 10000;
            if (age < 18 || age > 65) return false;
            return true;
        }
    }
}
