using MediatR;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Users.Queries.Results;

public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
{
    public string Id { get; set; }
    public GetUserByIdQuery(string id)
    {
        Id = id;
    }
}