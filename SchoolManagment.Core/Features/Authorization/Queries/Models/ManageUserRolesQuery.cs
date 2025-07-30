using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Data.Results;
namespace SchoolManagment.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public string UserId { get; set; }
    }
}
