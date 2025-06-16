using System.Security.Claims;
using System.Threading;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Route;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Extensions;
using ScoutRoute.Routes.Projects.Domain;
using ScoutRoute.Routes.Routes.Domain;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class CreateRouteEndpoint
    {
        public const string Name = "CreateRoute";

        public static IEndpointRouteBuilder MapCreateRoute(this IEndpointRouteBuilder app)
        {
            app.MapPost(
                    Contracts.Endpoints.Endpoints.Routes.CreateRoute,
                    async (
                        Guid projectId,
                        CreateRouteCommand command,
                        IDocumentStore store,
                        UserId ownerId,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        await using var session = await store.LightweightSerializableSessionAsync(
                            cancellationToken
                        );

                        var projId = new ProjectId(projectId);

                        var stream = await session.Events.FetchForWriting<ProjectAggregate>(
                            projId.GetStreamName(),
                            cancellationToken
                        );

                        var project = stream.Aggregate;

                        if (project is null)
                            return Results.NotFound();

                        RouteId routeId = new RouteId(command.Id);
                        var ev = RouteAggregate.CreateRoute(
                            projId,
                            routeId,
                            command.Name,
                            command.Stops.Select(s => new StopId(s))
                        );

                        session.Events.Append(routeId.GetStreamName(), ev);
                        await session.SaveChangesAsync();

                        return Results.NoContent();
                    }
                )
                .RequireAuthorization()
                .Produces(StatusCodes.Status204NoContent)
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
