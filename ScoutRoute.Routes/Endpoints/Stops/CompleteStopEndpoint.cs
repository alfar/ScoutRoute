using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;

namespace ScoutRoute.Routes.Endpoints.Stops
{
    internal static class CompleteStopEndpoint
    {
        public const string Name = "CompleteStop";

        public static IEndpointRouteBuilder MapCompleteStop(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Stops.CompleteStop, () =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
