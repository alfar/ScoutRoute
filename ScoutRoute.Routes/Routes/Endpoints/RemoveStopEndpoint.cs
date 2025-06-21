using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class RemoveStopEndpoint
    {
        public const string Name = "RemoveStop";

        public static IEndpointRouteBuilder MapRemoveStop(this IEndpointRouteBuilder app)
        {
            app.MapDelete(
                    Contracts.Endpoints.Endpoints.Routes.RemoveStop,
                    async (
                        Guid projectId,
                        Guid routeId,
                        Guid stopId,
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

                        var sId = new StopId(stopId);
                        var ev = route.RemoveStop(sId);

                        session.Events.Append(routeAggregateId.GetStreamName(), ev);
                        session.Events.Append(sId.GetStreamName(), ev);

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
