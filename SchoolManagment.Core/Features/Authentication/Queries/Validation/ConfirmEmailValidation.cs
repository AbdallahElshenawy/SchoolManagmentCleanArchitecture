using FluentValidation;
using SchoolManagment.Core.Features.Authentication.Queries.Models;
namespace SchoolManagment.Core.Features.Authentication.Queries.Validation
{
    public class ConfirmEmailValidation : AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailValidation()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("User id can't be emepty")
                 .NotNull().WithMessage("User id can't be null");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("code can't be emepty")
                .NotNull().WithMessage("code can't be null");
        }
    }
}
