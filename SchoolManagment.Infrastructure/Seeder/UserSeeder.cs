using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagment.Data.Entities.Identity;

namespace SchoolManagment.Infrastructure.Seeder.UserSeeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var usersCount = await userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@yahoo.com",
                    PhoneNumber = "201020205932",
                    Address = "Tanta",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(defaultuser, "String@123");
                await userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}