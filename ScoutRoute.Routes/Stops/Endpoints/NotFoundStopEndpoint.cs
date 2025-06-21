using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Stops.Domain;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class NotFoundStopEndpoint
    {
        public const string Name = "NotFoundStop";

        public static IEndpointRouteBuilder MapNotFoundStop(this IEndpointRouteBuilder app)
        {
            app.MapPut(
                    Contracts.Endpoints.Endpoints.Stops.NotFoundStop,
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

                        var @event = stop.MarkNotFound();
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
