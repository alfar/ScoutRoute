using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Endpoints.Projects;
using ScoutRoute.Routes.Endpoints.Routes;
using ScoutRoute.Routes.Endpoints.Stops;

namespace ScoutRoute.Routes.Endpoints
{
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MapRouteEndpoints(this IEndpointRouteBuilder app)
        {
            app
                .MapCreateProject()
                .MapUpdateProject()
                .MapDeleteProject()
                .MapGetProject()
                .MapGetAllProjects()
                
                .MapCreateRoute()
                .MapUpdateRoute()
                .MapDeleteRoute()
                .MapAddStop()
                .MapRemoveStop()
                .MapAssignTeam()
                .MapUnassignTeam()
                .MapGetAllRoutes()
                .MapGetRoute()

                .MapCreateStop()
                .MapCompleteStop()
                .MapDeleteStop()
                .MapAddComment()
                .MapGetAllStops()
                .MapGetUnassignedStops();

            return app;
        }
    }
}
