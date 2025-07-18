﻿using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class UnassignTeamEndpoint
    {
        public const string Name = "UnassignTeam";

        public static IEndpointRouteBuilder MapUnassignTeam(this IEndpointRouteBuilder app)
        {
            app.MapDelete(
                    Contracts.Endpoints.Endpoints.Routes.UnassignTeam,
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

                        // Example: Unassign team from route
                        var ev = route.UnassignTeam();

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
