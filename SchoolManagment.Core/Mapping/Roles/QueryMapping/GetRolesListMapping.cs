using Microsoft.AspNetCore.Identity;
using SchoolManagment.Core.Features.Authorization.Queries.Results;

namespace SchoolManagment.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<IdentityRole, GetRolesListResult>();
        }
    }
}
