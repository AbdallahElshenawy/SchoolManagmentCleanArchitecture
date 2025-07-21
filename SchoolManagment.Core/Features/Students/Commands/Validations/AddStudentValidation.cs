using FluentValidation;
using SchoolManagment.Core.Features.Students.Commands.Models;
using SchoolManagment.Service.Abstracts;
namespace SchoolManagment.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidation : AbstractValidator<AddStudentCommand>
    {
        private readonly IDepartmentService _departmentService;

        public AddStudentValidation(IDepartmentService departmentService)
        {
            _departmentService = departmentService;

            RuleFor(s => s.Name).NotEmpty().WithMessage("Name can't be empety")
                .NotNull()
                .MaximumLength(10);

            RuleFor(s => s.Address).NotEmpty().WithMessage("Address can't be empety")
                .NotNull()
                .MaximumLength(10);
            RuleFor(s => s.DepartmentId)
                .MustAsync(async (id, cancellation) =>
                {
                    if (id is null) return true;
                    var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
                    return department != null;
                })
                .WithMessage("Department not found");
        }
    }
}
