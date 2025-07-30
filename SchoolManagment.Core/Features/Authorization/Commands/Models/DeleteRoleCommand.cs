using MediatR;
using SchoolManagment.Core.Bases;

namespace SchoolManagment.Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public DeleteRoleCommand(string id)
        {
            Id = id;
        }
    }
}
