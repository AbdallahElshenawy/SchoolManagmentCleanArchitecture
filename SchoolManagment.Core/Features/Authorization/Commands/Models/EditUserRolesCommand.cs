using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Data.Dtos;

namespace SchoolManagment.Core.Features.Authorization.Commands.Models
{
    public class EditUserRolesCommand : EditUserRoles, IRequest<Response<string>>
    {
    }
}
