using FluentValidation;
using SchoolManagment.Core.Features.Authentication.Queries.Models;

namespace SchoolManagment.Core.Features.Authentication.Queries.Validation
{
    internal class ConfirmResetPasswordValidation : AbstractValidator<ConfirmResetPasswordQuery>
    {
        public ConfirmResetPasswordValidation()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email can't be emepty")
                 .NotNull().WithMessage("Email can't be null");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("code can't be emepty")
                .NotNull().WithMessage("code can't be null");
        }
    }
}