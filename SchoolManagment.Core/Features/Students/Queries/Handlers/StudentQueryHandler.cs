using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagment.Core.Bases;
using SchoolManagment.Core.Extensions;
using SchoolManagment.Core.Features.Students.Queries.Models;
using SchoolManagment.Core.Features.Students.Queries.Results;
using SchoolManagment.Core.Resources;
using SchoolManagment.Data.Entities;
using SchoolManagment.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolManagment.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : ResponseHandler,
                                    IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                    IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>,
                                    IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await studentService.GetStudentsAsync();
            var studentResponseList = mapper.Map<List<GetStudentListResponse>>(studentList);
            var result = Success(studentResponseList);
            result.Meta = new { Count = studentResponseList.Count };
            return result;
        }

        public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await studentService.GetStudentByIdAsync(request.Id);
            if (student is null)
                return NotFound<GetStudentResponse>(stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = mapper.Map<GetStudentResponse>(student);
            return Success(result);

        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = s => new GetStudentPaginatedListResponse(
                s.StudID,
                s.Name,
                s.Address,
                s.Department.DName
            );
            var FilterQuery = studentService.FilterStudentPaginatedQuerable(
                request.SearchBy,
                request.OrderBy
            );
            var PaginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }

    }
}
