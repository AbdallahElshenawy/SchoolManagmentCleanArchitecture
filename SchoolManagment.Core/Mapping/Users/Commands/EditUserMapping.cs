using SchoolManagment.Core.Features.Users.Commands.Models;
using SchoolManagment.Data.Entities.Identity;

namespace SchoolManagment.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
