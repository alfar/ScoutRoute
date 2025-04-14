using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class DeleteRouteEndpoint
    {
        public const string Name = "DeleteRoute";

        public static IEndpointRouteBuilder MapDeleteRoute(this IEndpointRouteBuilder app)
        {
            app
                .MapDelete(Contracts.Endpoints.Endpoints.Routes.DeleteRoute, (Guid projectId, Guid routeId) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
