using Microsoft.Extensions.DependencyInjection;
using SchoolManagment.Service.Abstracts;
using SchoolManagment.Service.Implementations;
namespace SchoolManagment.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddModuleServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IInstructorService, InstructorService>();
            services.AddTransient<IFileService, FileService>();




            return services;
        }
    }
}
