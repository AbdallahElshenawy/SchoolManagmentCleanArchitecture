using AutoMapper;
using SchoolManagment.Core.Features.Instructors.Commands.Models;
namespace SchoolManagment.Core.Mapping.Instructors.Commands
{
    public class AddInstructorMapping : Profile
    {
        public AddInstructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

        }
    }
}
