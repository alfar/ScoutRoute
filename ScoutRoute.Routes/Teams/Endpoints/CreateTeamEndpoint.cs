using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Teams;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Teams.Domain;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Teams.Endpoints
{
    internal static class CreateTeamEndpoint
    {
        public const string Name = "CreateTeam";


        public static IEndpointRouteBuilder MapCreateTeam(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Teams.CreateTeam, async (Guid projectId, CreateTeamCommand command, IDocumentStore store, UserId userId, CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var ev = TeamAggregate.CreateTeam(new ProjectId(projectId), new TeamId(command.TeamId));

                        using var session = await store.LightweightSerializableSessionAsync(cancellationToken);

                        session.Events.StartStream(ev.TeamId.GetStreamName(), ev);

                        await session.SaveChangesAsync(cancellationToken);

                        return Results.NoContent();
                    }
                    catch (Exception ex)
                    {
                        return Results.BadRequest(ex);
                    }
                })
                .RequireAuthorization()
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status204NoContent)
                .WithName(Name)
                .WithTags("Teams");

            return app;
        }
    }
}
