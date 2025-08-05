using FluentValidation;
using SchoolManagment.Core.Features.Authentication.Commands.Models;

namespace SchoolManagment.Core.Features.Authentication.Commands.Validations
{
    public class ResetPasswordValidation : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .NotNull().WithMessage("Email can't be null")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .NotNull().WithMessage("Password can't be null")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Confirm password must match the password");
        }
    }
}
