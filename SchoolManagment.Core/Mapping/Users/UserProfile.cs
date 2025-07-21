using AutoMapper;

namespace SchoolManagment.Core.Mapping.Users
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserMapping();
        }
    }

}
