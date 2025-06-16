using JasperFx.CodeGeneration.Frames;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Extensions;
using ScoutRoute.Routes.Projects.Domain;
using ScoutRoute.Routes.Stops.Domain;
using ScoutRoute.Shared.ValueTypes;
using System.Security.Claims;
using System.Threading;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class CreateStopEndpoint
    {
        public const string Name = "CreateStop";

        public static IEndpointRouteBuilder MapCreateStop(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Stops.CreateStop, async (Guid projectId, CreateStopCommand command, IDocumentStore store, ClaimsPrincipal user, CancellationToken cancellationToken) =>
                {
                    return await user.LoggedIn(async ownerId =>
                    {
                        var session = await store.LightweightSerializableSessionAsync(cancellationToken);
                        var id = new ProjectId(projectId);

                        var ev = StopAggregate.Create(id, new StopId(command.Id), command.ContactPerson, command.Title, command.Quantity, command.Latitude, command.Longitude, command.Comment, ownerId);

                        session.Events.Append(id.GetStreamName(), ev);

                        await session.SaveChangesAsync(cancellationToken);
                        return Results.NoContent();
                    });
                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
