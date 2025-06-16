using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class CompleteStopEndpoint
    {
        public const string Name = "CompleteStop";

        public static IEndpointRouteBuilder MapCompleteStop(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Stops.CompleteStop, (Guid projectId, Guid stopId) =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
