using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class UnassignTeamEndpoint
    {
        public const string Name = "UnassignTeam";

        public static IEndpointRouteBuilder MapUnassignTeam(this IEndpointRouteBuilder app)
        {
            app
                .MapDelete(Contracts.Endpoints.Endpoints.Routes.UnassignTeam, (Guid projectId, Guid routeId) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");
            return app;
        }
    }
}
