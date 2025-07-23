using FluentValidation;
using SchoolManagment.Core.Features.Users.Commands.Models;

namespace SchoolManagment.Core.Features.Users.Commands.Validations
{
    public class EditUserValidation : AbstractValidator<EditUserCommand>
    {
        public EditUserValidation()
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("User name is required.")
               .MinimumLength(3).WithMessage("User name must be at least 3 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[0-9]{10,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Phone number must be valid and contain 10 to 15 digits.");

        }
    }
}
