﻿using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Stops.Domain;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class CompleteStopEndpoint
    {
        public const string Name = "CompleteStop";

        public static IEndpointRouteBuilder MapCompleteStop(this IEndpointRouteBuilder app)
        {
            app.MapPut(
                    Contracts.Endpoints.Endpoints.Stops.CompleteStop,
                    async (
                        Guid projectId,
                        Guid stopId,
                        IDocumentStore store,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        using var session = store.LightweightSession();
                        var stop = await session.LoadAsync<StopAggregate>(
                            stopId,
                            cancellationToken
                        );
                        if (stop == null)
                        {
                            return Results.NotFound();
                        }

                        var @event = stop.MarkCompleted();
                        session.Events.Append(stopId, @event);
                        await session.SaveChangesAsync(cancellationToken);

                        return Results.NoContent();
                    }
                )
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
