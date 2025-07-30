using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Data.Results;

namespace SchoolManagment.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        public string UserId { get; set; }
    }
}
