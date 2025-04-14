using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;

namespace ScoutRoute.Routes.Endpoints.Stops
{
    internal static class DeleteStopEndpoint
    {
        public const string Name = "DeleteStop";

        public static IEndpointRouteBuilder MapDeleteStop(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Stops.DeleteStop, () =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
