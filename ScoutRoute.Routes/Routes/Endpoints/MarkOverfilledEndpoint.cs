using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints;

internal static class MarkOverfilledEndpoint
{
    public const string Name = "MarkOverfilled";

    public static IEndpointRouteBuilder MapMarkOverfilled(this IEndpointRouteBuilder app)
    {
        app.MapPut(
                Contracts.Endpoints.Endpoints.Routes.MarkOverfilled,
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

                    var routeAggregateId = new RouteId(routeId);

                    var routeAggregate = await session.Events.FetchForWriting<RouteAggregate>(
                        routeAggregateId.GetStreamName(),
                        cancellationToken
                    );

                    var route = routeAggregate.Aggregate;

                    if (route is null)
                        return Results.NotFound();

                    var ev = route.MarkOverfilled();

                    session.Events.Append(routeAggregateId.GetStreamName(), ev);

                    await session.SaveChangesAsync(cancellationToken);

                    return Results.NoContent();
                }
            )
            .WithName(Name)
            .WithTags("Routes");
        return app;
    }
}
