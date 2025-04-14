using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class AssignTeamEndpoint
    {
        public const string Name = "AssignTeam";

        public static IEndpointRouteBuilder MapAssignTeam(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Routes.AssignTeam, (Guid projectId, Guid routeId, AssignTeamCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");
            return app;
        }
    }
}
