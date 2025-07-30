using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Data.Dtos;

namespace SchoolManagment.Core.Features.Authorization.Commands.Models
{
    public class EditUserClaimsCommand : EditUserClaims, IRequest<Response<string>>
    {
    }
}
