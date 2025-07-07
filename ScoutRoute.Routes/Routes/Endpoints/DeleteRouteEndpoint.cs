using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class DeleteRouteEndpoint
    {
        public const string Name = "DeleteRoute";

        public static IEndpointRouteBuilder MapDeleteRoute(this IEndpointRouteBuilder app)
        {
            app.MapDelete(
                    Contracts.Endpoints.Endpoints.Routes.DeleteRoute,
                    async (
                        Guid projectId,
                        Guid routeId,
                        IDocumentStore store,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        await using var session = await store.LightweightSerializableSessionAsync(
                            cancellationToken
                        );

                        var route = await session.LoadAsync<RouteAggregate>(
                            routeId,
                            cancellationToken
                        );
                        if (route == null)
                        {
                            return Results.NotFound();
                        }
                        route.Delete();
                        session.Delete(route);
                        await session.SaveChangesAsync(cancellationToken);
                        return Results.NoContent();
                    }
                )
                .RequireAuthorization()
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
