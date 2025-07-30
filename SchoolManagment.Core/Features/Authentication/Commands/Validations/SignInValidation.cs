using FluentValidation;
using SchoolManagment.Core.Features.Authentication.Commands.Models;

namespace SchoolManagment.Core.Features.Authentication.Commands.Validations
{
    public class SignInValidation : AbstractValidator<SignInCommand>
    {
        public SignInValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().NotNull().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().NotNull().WithMessage("Password is required.");

        }
    }
}
