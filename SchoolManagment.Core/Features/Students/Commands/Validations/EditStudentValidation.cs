using FluentValidation;
using SchoolManagment.Core.Features.Students.Commands.Models;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidation : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;

        public EditStudentValidation(IStudentService studentService)
        {
            _studentService = studentService;

            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Name can't be empty")
                .NotNull()
                .MaximumLength(10)
                .MustAsync(BeUniqueName).WithMessage("This name already exists for another student");

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage("Address can't be empty")
                .NotNull()
                .MaximumLength(10);
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentsAsync();
            return student.All(s => s.Name != name);
        }
    }
}
