using Blog.Dto.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Dto.Validators.User
{
    public class FollowValidator : AbstractValidator<UserFollowDto>
    {
        public FollowValidator()
        {
            RuleFor(x => x.TargetUserId)
                .NotNull().WithMessage("{PropertyName} must not be null");
        }
    }
}
