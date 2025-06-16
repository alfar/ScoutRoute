using Marten;
using Marten.Events.Projections;
using Microsoft.Extensions.DependencyInjection;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Routes.Routes.Projections;
using ScoutRoute.Routes.Stops.Projections;
using ScoutRoute.Routes.Teams.Projections;

namespace ScoutRoute.Routes.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRouteServices(this IServiceCollection services)
        {
            services.AddMarten(options =>
            {
                options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsString;

                options.Projections.Add(new ProjectListProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new ProjectProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new StopProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new RouteProjection(), ProjectionLifecycle.Inline);
                options.Projections.Add(new TeamProjection(), ProjectionLifecycle.Inline);
            }
            ).UseNpgsqlDataSource();

            return services;
        }
    }
}
