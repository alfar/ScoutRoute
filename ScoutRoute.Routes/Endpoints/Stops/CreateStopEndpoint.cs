using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;

namespace ScoutRoute.Routes.Endpoints.Stops
{
    internal static class CreateStopEndpoint
    {
        public const string Name = "CreateStop";

        public static IEndpointRouteBuilder MapCreateStop(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Stops.CreateStop, (CreateStopCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
