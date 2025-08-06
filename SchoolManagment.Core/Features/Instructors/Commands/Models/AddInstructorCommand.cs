using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagment.Core.Bases;

namespace SchoolManagment.Core.Features.Instructors.Commands.Models
{
    public class AddInstructorCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Postion { get; set; }
        public int? SuperVisorId { get; set; }
        public decimal Salary { get; set; }
        public IFormFile? Image { get; set; }
        public int DID { get; set; }
        public int? DepartmentDID { get; set; }


    }
}
