using SchoolManagment.Core.Features.Users.Queries.Results;
using SchoolManagment.Data.Entities.Identity;

namespace SchoolManagment.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
