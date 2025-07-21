using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Features.Departments.Queries.Models;
using SchoolManagment.Core.Features.Students.Queries.Handlers;
using SchoolManagment.Core.Features.Students.Queries.Results;
using SchoolManagment.Core.Resources;
using SchoolManagment.Service.Abstracts;
namespace SchoolManagment.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler(IDepartmentService departmentService, IMapper mapper, IStringLocalizer<StudentQueryHandler> stringLocalizer) : ResponseHandler,
        //IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentResponse>>
    // IRequestHandler<GetDepartmentPaginatedListQuery, PaginatedResult<GetDepartmentPaginatedListResponse>>
    {
        public async Task<Response<GetDepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await departmentService.GetDepartmentByIdAsync(request.Id);
            if (response == null)
                return NotFound<GetDepartmentResponse>(stringLocalizer[SharedResourcesKeys.NotFound]);
            var mappingResponse = mapper.Map<GetDepartmentResponse>(response);
            return Success(mappingResponse);

        }
    }
}
