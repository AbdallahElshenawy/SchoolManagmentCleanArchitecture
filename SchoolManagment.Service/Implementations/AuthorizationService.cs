using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Dtos;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Data.Helper;
using SchoolManagment.Data.Results;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Service.Abstracts;
using System.Security.Claims;

namespace SchoolManagment.Service.Implementations
{
    public class AuthorizationService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, AppDbContext context) : IAuthorizationService
    {
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identyRole = new IdentityRole();
            identyRole.Name = roleName;
            var result = await roleManager.CreateAsync(identyRole);
            if (result.Succeeded)
                return "Role created successfully";
            return string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> DeleteRoleAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
                return "Role not found";
            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            if (usersInRole.Count() > 0)
            {
                return "Cannot delete role because there are users assigned to it";
            }
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Role deleted successfully";
            return string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> EditRoleAsync(string id, string roleName)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
                return "Role not found";
            role.Name = roleName;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return "Role updated successfully";
            return string.Join(", ", result.Errors.Select(e => e.Description));

        }

        public async Task<string> EditUserRoles(EditUserRoles request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return "User Not Found";
            var userRoles = await userManager.GetRolesAsync(user);
            var removedRoles = await userManager.RemoveFromRolesAsync(user, userRoles);
            if (!removedRoles.Succeeded)
                return "Failed To Remove Old Roles";
            var selectedRoles = request.userRoles.Where(s => s.HasRole == true).Select(s => s.Name);
            var result = await userManager.AddToRolesAsync(user, selectedRoles);
            if (!result.Succeeded)
                return " Failed To Add New Roles";
            return "Success";
        }


        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        public async Task<ManageUserRolesResult> GetUserRoles(User user)
        {
            var response = new ManageUserRolesResult();
            var userRolesList = new List<UserRoles>();
            var roles = await userManager.GetRolesAsync(user);
            var allRoles = await roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in allRoles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (roles.Contains(role.Name))
                {
                    userRole.HasRole = true;
                }
                else
                {
                    userRole.HasRole = false;
                }
                userRolesList.Add(userRole);
            }
            response.userRoles = userRolesList;
            return response;

        }

        public async Task<bool> IsRoleExistByIdAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            return role != null;
        }

        public async Task<bool> IsRoleExistByNameAsync(string roleName)
        {
            return await roleManager.RoleExistsAsync(roleName);
        }
        public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResult();
            var usercliamsList = new List<UserClaims>();
            response.UserId = user.Id;

            var userClaims = await userManager.GetClaimsAsync(user);

            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }
                usercliamsList.Add(userClaim);
            }
            response.userClaims = usercliamsList;
            return response;
        }
        public async Task<string> EditUserClaims(EditUserClaims request)
        {
            var transact = await context.Database.BeginTransactionAsync();
            try
            {
                var user = await userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }

                var userClaims = await userManager.GetClaimsAsync(user);
                var removeClaimsResult = await userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimsResult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

                var addUserClaimResult = await userManager.AddClaimsAsync(user, claims);
                if (!addUserClaimResult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
    }
}
