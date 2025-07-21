using MediatR;
using SchoolManagment.Core.Extensions;
using SchoolManagment.Core.Features.Users.Queries.Results;

namespace SchoolManagment.Core.Features.Users.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
