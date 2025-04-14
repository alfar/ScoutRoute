using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class RemoveStopEndpoint
    {
        public const string Name = "RemoveStop";

        public static IEndpointRouteBuilder MapRemoveStop(this IEndpointRouteBuilder app)
        {
            app
                .MapDelete(Contracts.Endpoints.Endpoints.Routes.RemoveStop, (Guid projectId, Guid routeId) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");
            return app;
        }
    }
}
