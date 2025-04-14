using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Endpoints.Stops
{
    internal static class GetAllStopsEndpoint
    {
        public const string Name = "GetAllStops";

        public static IEndpointRouteBuilder MapGetAllStops(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Stops.GetAll, () =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");

            return app;
        }
    }
}
