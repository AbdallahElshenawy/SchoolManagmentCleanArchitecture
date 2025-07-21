using SchoolManagment.Core.Features.Students.Queries.Results;
using SchoolManagment.Data.Entities;

namespace SchoolManagment.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentResponseMapping()
        {
            CreateMap<Student, GetStudentResponse>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.DName));
        }  
    }
}
