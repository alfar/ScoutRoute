using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class AddStopEndpoint
    {
        public const string Name = "AddStop";

        public static IEndpointRouteBuilder MapAddStop(this IEndpointRouteBuilder app)
        {
            app.MapPost(
                    Contracts.Endpoints.Endpoints.Routes.AddStop,
                    async (
                        Guid projectId,
                        Guid routeId,
                        AddStopCommand command,
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

                        StopId stopId = new(command.StopId);
                        var ev = route.AddStop(stopId);

                        session.Events.Append(routeAggregateId.GetStreamName(), ev);
                        session.Events.Append(stopId.GetStreamName(), ev);

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
