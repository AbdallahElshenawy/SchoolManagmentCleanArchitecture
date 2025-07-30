using Microsoft.AspNetCore.Identity;
using SchoolManagment.Data.Dtos;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Results;
namespace SchoolManagment.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<string> EditRoleAsync(string roleId, string roleName);
        Task<string> DeleteRoleAsync(string roleId);
        Task<bool> IsRoleExistByNameAsync(string roleName);
        Task<bool> IsRoleExistByIdAsync(string roleId);
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleByIdAsync(string id);
        Task<string> EditUserRoles(EditUserRoles request);
        Task<ManageUserRolesResult> GetUserRoles(User user);
        Task<ManageUserClaimsResult> ManageUserClaimData(User user);
        Task<string> EditUserClaims(EditUserClaims request);

    }
}
