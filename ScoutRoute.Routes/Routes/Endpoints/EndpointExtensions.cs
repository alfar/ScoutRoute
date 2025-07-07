using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Routes.Endpoints;

namespace ScoutRoute.Routes.Endpoints
{
    public static class EndpointExtensions
    {
        public static void MapRouteEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapAddComment();
            endpoints.MapAddStop();
            endpoints.MapAssignTeam();
            endpoints.MapChangeExtraStops();
            endpoints.MapCreateRoute();
            endpoints.MapDeleteRoute();
            endpoints.MapGetAllRoutes();
            endpoints.MapGetRoute();
            endpoints.MapGetRoutesForTeamEndpoint();
            endpoints.MapMarkComplete();
            endpoints.MapMarkOverfilled();
            endpoints.MapRemoveStop();
            endpoints.MapResetStatus();
            endpoints.MapUnassignTeam();
            endpoints.MapUpdateRoute();
        }
    }
}
