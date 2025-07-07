using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Endpoints;

internal static class AddCommentEndpoint
{
    public const string Name = "AddComment";

    public static IEndpointRouteBuilder MapAddComment(this IEndpointRouteBuilder app)
    {
        app.MapPost(
                Contracts.Endpoints.Endpoints.Routes.AddComment,
                async (
                    Guid projectId,
                    Guid routeId,
                    AddCommentCommand command,
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

                    var ev = route.AddComment(command.Comment);

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
