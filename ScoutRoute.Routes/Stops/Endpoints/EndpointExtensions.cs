using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapStopEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapCompleteStop()  
                .MapCreateStop()
                .MapDeleteStop()
                .MapGetAllStops()
                .MapGetUnassignedStops()
                .MapNotFoundStop();

            return endpoints;
        }
    }
}
