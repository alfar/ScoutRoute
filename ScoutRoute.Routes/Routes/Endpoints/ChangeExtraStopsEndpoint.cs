using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints;

internal static class ChangeExtraStopsEndpoint
{
    public const string Name = "ChangeExtraStops";

    public static IEndpointRouteBuilder MapChangeExtraStops(this IEndpointRouteBuilder app)
    {
        app.MapPut(
                Contracts.Endpoints.Endpoints.Routes.ChangeExtraStops,
                async (
                    Guid projectId,
                    Guid routeId,
                    ChangeExtraStopsCommand command,
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

                    var ev = route.ChangeExtraStops(command.ExtraStops);

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
