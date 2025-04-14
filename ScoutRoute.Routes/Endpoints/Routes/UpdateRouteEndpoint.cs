using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class UpdateRouteEndpoint
    {
        public const string Name = "UpdateRoute";

        public static IEndpointRouteBuilder MapUpdateRoute(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Routes.UpdateRoute, (Guid projectId, Guid routeId, UpdateRouteCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
