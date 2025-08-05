using FluentValidation;
using SchoolManagment.Core.Features.Authentication.Commands.Models;

namespace SchoolManagment.Core.Features.Authentication.Commands.Validations
{
    public class SendResetPasswordValidation : AbstractValidator<SendResetPasswordCommand>
    {
        public SendResetPasswordValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .NotNull().WithMessage("Email can't be null")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
