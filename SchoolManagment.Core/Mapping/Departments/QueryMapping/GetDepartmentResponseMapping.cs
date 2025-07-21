using SchoolManagment.Core.Features.Students.Queries.Results;
using SchoolManagment.Data.Entities;

namespace SchoolManagment.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.DepartmentManager, opt => opt.MapFrom(src => src.InsManager.Name))
                .ForMember(ds => ds.Subjects, opt => opt.MapFrom(src => src.DepartmentSubjects))
                .ForMember(ds => ds.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(ds => ds.Instructors, opt => opt.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.SubjectName));

            CreateMap<Student, StudentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            CreateMap<Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
