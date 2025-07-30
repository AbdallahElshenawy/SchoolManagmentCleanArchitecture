using FluentValidation;
using SchoolManagment.Core.Features.Authorization.Commands.Models;

namespace SchoolManagment.Core.Features.Authorization.Commands.Validation
{
    public class EditRoleValidation : AbstractValidator<EditRoleCommand>
    {
        public EditRoleValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().NotNull().WithMessage("Id is required.");
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Role name is required.");

        }
    }
}
