using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Students.Queries.Results;

namespace SchoolManagment.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentResponse>>
    {
        public int Id { get; set; }
        public GetDepartmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
