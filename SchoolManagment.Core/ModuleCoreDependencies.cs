using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagment.Core.Behaviors;
using System.Reflection;

namespace SchoolManagment.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddModuleCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
