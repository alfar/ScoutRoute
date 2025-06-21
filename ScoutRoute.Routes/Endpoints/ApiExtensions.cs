using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Projects.Endpoints;
using ScoutRoute.Routes.Routes.Endpoints;
using ScoutRoute.Routes.Stops.Endpoints;
using ScoutRoute.Routes.Teams.Endpoints;

namespace ScoutRoute.Routes.Endpoints
{
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MapRouteEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapCreateProject()
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
                .MapMarkComplete()
                .MapMarkOverfilled()
                .MapChangeExtraStops()
                .MapAddComment()
                .MapGetAllRoutes()
                .MapGetRoute()
                .MapGetRoutesForTeamEndpoint()
                .MapCreateStop()
                .MapCompleteStop()
                .MapDeleteStop()
                .MapGetAllStops()
                .MapGetUnassignedStops()
                .MapCreateTeam()
                .MapUpdateTeamName()
                .MapUpdateTeamLead()
                .MapUpdateTeamPhone()
                .MapUpdateTeamTrailerType()
                .MapAddTeamMember()
                .MapRemoveTeamMember()
                .MapGetAllTeams()
                .MapGetTeam()
            //                .MapRebuildTeamProjection()
            ;

            return app;
        }
    }
}
