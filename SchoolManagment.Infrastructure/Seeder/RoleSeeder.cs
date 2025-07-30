using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace SchoolManagment.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            var rolesCount = await roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {

                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "User"
                });
            }
        }
    }
}
