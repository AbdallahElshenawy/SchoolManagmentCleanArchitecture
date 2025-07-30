using MediatR;
using SchoolManagment.Core.Extensions;
using SchoolManagment.Core.Features.Students.Queries.Results;
using SchoolManagment.Data.Enums;

namespace SchoolManagment.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? SearchBy { get; set; }
    }
}
