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


            return services;
        }
    }
}
