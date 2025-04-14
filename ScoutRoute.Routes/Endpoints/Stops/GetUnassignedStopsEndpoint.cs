using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Endpoints.Stops
{
    internal static class GetUnassignedStopsEndpoint
    {
        public const string Name = "GetUnassignedStops";

        public static IEndpointRouteBuilder MapGetUnassignedStops(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Stops.GetUnassigned, () =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");

            return app;
        }
    }
}
