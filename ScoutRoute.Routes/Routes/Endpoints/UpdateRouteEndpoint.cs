using System.Threading;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class UpdateRouteEndpoint
    {
        public const string Name = "UpdateRoute";

        public static IEndpointRouteBuilder MapUpdateRoute(this IEndpointRouteBuilder app)
        {
            app.MapPut(
                    Contracts.Endpoints.Endpoints.Routes.UpdateRoute,
                    async (
                        Guid projectId,
                        Guid routeId,
                        UpdateRouteCommand command,
                        IDocumentStore store,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        await using var session = await store.LightweightSerializableSessionAsync(
                            cancellationToken
                        );

                        var routeAggregateId = new RouteId(routeId);

                        var routeAggregate = await session.Events.FetchForWriting<RouteAggregate>(
                            routeAggregateId.GetStreamName(),
                            cancellationToken
                        );

                        var route = routeAggregate.Aggregate;

                        if (route is null)
                            return Results.NotFound();

                        // Example: Update route name
                        var ev = route.UpdateName(command.Name);

                        session.Events.Append(routeAggregateId.GetStreamName(), ev);

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
