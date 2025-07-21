using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagment.Core.Features.Students.Commands.Models;
using SchoolManagment.Core.Features.Students.Queries.Models;
using static SchoolManagment.Data.AppMetaData.Routing;

namespace SchoolManagment.Api.Controllers
{
    [ApiController]
    public class StudentController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet(StudentRouting.GetStudents)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        [HttpGet(StudentRouting.paginatedList)]
        public async Task<IActionResult> paginatedList([FromQuery] GetStudentPaginatedListQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(StudentRouting.GetStudentById)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var response = await mediator.Send(new GetStudentByIdQuery() { Id = id });
            return NewResult(response);
        }
        [HttpPost(StudentRouting.Create)]
        public async Task<IActionResult> CreatStudent(AddStudentCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(StudentRouting.Edit)]
        public async Task<IActionResult> EditStudent(EditStudentCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete(StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            var response = await mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}







/* public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
{
    var query = studentService.GetStudentsAsQuerable();

    // Defensive: Ensure query is not null
    if (query == null)
        return PaginatedResult<GetStudentPaginatedListResponse>.Success([], 0, request.PageNumber, request.PageSize);

    // Optional: Apply search
    if (!string.IsNullOrWhiteSpace(request.SearchBy))
    {
        var search = request.SearchBy.Trim().ToLower();
        query = query.Where(e =>
            e.Name.ToLower().Contains(search) ||
            e.Address.ToLower().Contains(search) ||
            (e.Department != null && e.Department.DName.ToLower().Contains(search))
        );
    }

    // Optional: Apply ordering
    if (request.OrderBy != null && request.OrderBy.Length > 0)
    {
        foreach (var order in request.OrderBy)
        {
            if (order.Equals("Name", StringComparison.OrdinalIgnoreCase))
                query = query.OrderBy(e => e.Name);
            else if (order.Equals("Department", StringComparison.OrdinalIgnoreCase))
                query = query.OrderBy(e => e.Department.DName);
            // Add more ordering options as needed
        }
    }
    else
    {
        query = query.OrderBy(e => e.StudID); // Default ordering
    }

    // Use AsNoTracking for read-only queries
    query = query.AsNoTracking();

    // Projection with null checks
    var expression = e => new GetStudentPaginatedListResponse(
        e.StudID,
        e.Name,
        e.Address,
        e.Department != null ? e.Department.DName : null
    );

    var paginatedList = await query.Select(expression)
        .ToPaginatedListAsync(request.PageNumber, request.PageSize);

    return paginatedList;
}
*/