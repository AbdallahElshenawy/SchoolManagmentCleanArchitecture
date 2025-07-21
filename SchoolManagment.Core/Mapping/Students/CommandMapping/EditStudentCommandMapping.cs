using SchoolManagment.Core.Features.Students.Commands.Models;
using SchoolManagment.Data.Entities;

namespace SchoolManagment.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                    .ForMember(dest => dest.DID,
                    opt => opt.MapFrom(src => src.DepartmentId))
                    .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));

        }
    }
}

