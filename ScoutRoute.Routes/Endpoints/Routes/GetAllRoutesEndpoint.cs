using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Endpoints.Routes
{
    internal static class GetAllRoutesEndpoint
    {
        public const string Name = "GetAllRoutes";

        public static IEndpointRouteBuilder MapGetAllRoutes(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Routes.GetAll, () =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
