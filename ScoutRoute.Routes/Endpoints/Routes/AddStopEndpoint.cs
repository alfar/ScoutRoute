using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class AddStopEndpoint
    {
        public const string Name = "AddStop";

        public static IEndpointRouteBuilder MapAddStop(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Routes.AddStop, (Guid projectId, Guid routeId, AddStopCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");
            return app;
        }
    }
}
