using Microsoft.Extensions.DependencyInjection;
using ScoutRoute.Routes.Repository;
using ScoutRoute.Routes.Repository.ReadModels;

namespace ScoutRoute.Routes.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRouteServices(this IServiceCollection services)
        {
            services.AddSingleton<IProjectCache, ProjectCache>();
            services.AddSingleton<ProjectSubscriber>();
            services.AddScoped<IProjectWriter, ProjectWriter>();
            services.AddScoped<IProjectReader, ProjectReader>();

            return services;
        }
    }
}
