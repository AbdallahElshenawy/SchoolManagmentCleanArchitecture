using FluentValidation;
using SchoolManagment.Core.Features.Email.Commands.Models;

namespace SchoolManagment.Core.Features.Email.Commands.Validations
{
    public class SendEmailValidation : AbstractValidator<SendEmailCommand>
    {
        public SendEmailValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .NotNull()
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .NotNull()
                .MinimumLength(10).WithMessage("Message must be at least 10 characters long.")
                .MaximumLength(500).WithMessage("Message must not exceed 500 characters.");
        }
    }
}
