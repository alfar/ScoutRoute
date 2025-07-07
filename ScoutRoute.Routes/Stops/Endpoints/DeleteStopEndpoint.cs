using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Stops.Domain;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class DeleteStopEndpoint
    {
        public const string Name = "DeleteStop";

        public static IEndpointRouteBuilder MapDeleteStop(this IEndpointRouteBuilder app)
        {
            app.MapDelete(
                    Contracts.Endpoints.Endpoints.Stops.DeleteStop,
                    async (
                        Guid projectId,
                        Guid stopId,
                        IDocumentStore store,
                        UserId userId,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        var session = await store.LightweightSerializableSessionAsync(
                            cancellationToken
                        );
                        var id = new StopId(stopId);

                        var stream = await session.Events.FetchForWriting<StopAggregate>(
                            id.GetStreamName(),
                            cancellationToken
                        );

                        var stop = stream.Aggregate;
                        if (stop is null)
                            return Results.NotFound();

                        var ev = stop.Delete();

                        session.Events.Append(id.GetStreamName(), ev);
                        await session.SaveChangesAsync(cancellationToken);
                        return Results.NoContent();
                    }
                )
                .RequireAuthorization()
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
