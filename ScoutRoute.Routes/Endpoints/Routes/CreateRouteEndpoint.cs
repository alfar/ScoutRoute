using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class CreateRouteEndpoint
    {
        public const string Name = "CreateRoute";

        public static IEndpointRouteBuilder MapCreateRoute(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Routes.CreateRoute, (Guid projectId, CreateRouteCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
