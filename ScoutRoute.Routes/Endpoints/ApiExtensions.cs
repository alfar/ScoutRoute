using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Projects.Endpoints;
using ScoutRoute.Routes.Routes.Endpoints;
using ScoutRoute.Routes.Stops.Endpoints;
using ScoutRoute.Routes.Teams.Endpoints;

namespace ScoutRoute.Routes.Endpoints
{
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MapRoutesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapProjectEndpoints();
            app.MapRouteEndpoints();
            app.MapStopEndpoints();
            app.MapTeamEndpoints();
            return app;
        }
    }
}
