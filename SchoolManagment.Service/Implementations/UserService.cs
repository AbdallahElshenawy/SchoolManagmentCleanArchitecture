using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagment.Data.Entities.Identity;
using SchoolManagment.Infrastructure.Data;
using SchoolManagment.Service.Abstracts;

namespace SchoolManagment.Service.Implementations
{
    public class UserService(
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IEmailService emailService,
        AppDbContext context
                            ) : IUserService
    {

        public async Task<string> AddUserAsync(User user, string password)
        {
            var httpContext = httpContextAccessor.HttpContext;

            var urlHelperFactory = httpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
            var trans = await context.Database.BeginTransactionAsync();

            try
            {
                var existUser = await userManager.FindByEmailAsync(user.Email);
                if (existUser != null)
                    return "EmailIsExist";

                var userByUserName = await userManager.FindByNameAsync(user.UserName);
                if (userByUserName != null)
                    return "UserNameIsExist";

                var newUser = await userManager.CreateAsync(user, password);
                if (!newUser.Succeeded)
                    return string.Join(",", newUser.Errors.Select(x => x.Description));

                await userManager.AddToRoleAsync(user, "User");

                // Generate confirmation token
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                // Generate confirmation URL
                var request = httpContextAccessor.HttpContext.Request;
                var actionUrl = urlHelper.Action(new UrlActionContext
                {
                    Action = "ConfirmEmail",
                    Controller = "Auth",
                    Values = new { userId = user.Id, code = code }
                });

                var confirmationLink = request.Scheme + "://" + request.Host + actionUrl;
                var message = $"To confirm your email, click the link: <a href='{confirmationLink}'>Confirm Email</a>";

                await emailService.SendEmail(user.Email, message, "Confirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
    }
}
