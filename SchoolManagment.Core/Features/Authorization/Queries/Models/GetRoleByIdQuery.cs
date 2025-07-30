using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Authorization.Queries.Results;

namespace SchoolManagment.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResult>>
    {
        public string Id { get; set; }
    }
}
