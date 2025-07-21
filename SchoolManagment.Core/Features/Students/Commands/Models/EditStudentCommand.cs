using MediatR;

namespace SchoolManagment.Core.Features.Students.Commands.Models
{
    public class EditStudentCommand : AddStudentCommand, IRequest<string>
    {
        public int Id { get; set; }
    }
}
