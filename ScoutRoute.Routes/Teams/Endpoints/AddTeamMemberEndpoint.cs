using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Teams;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Teams.Domain;

namespace ScoutRoute.Routes.Teams.Endpoints
{
    internal static class AddTeamMemberEndpoint
    {
        public const string Name = "AddTeamMember";


        public static IEndpointRouteBuilder MapAddTeamMember(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Teams.AddTeamMember, async (Guid projectId, Guid teamId, AddTeamMemberCommand command, IDocumentStore store, CancellationToken cancellationToken) =>
                {
                    try
                    {
                        using var session = await store.LightweightSerializableSessionAsync(cancellationToken);

                        var teamAggregateId = new TeamId(teamId);

                        var teamAggregate = await session.Events.FetchForWriting<TeamAggregate>(teamAggregateId.GetStreamName(), cancellationToken);

                        var team = teamAggregate.Aggregate;

                        if (team is null) return Results.NotFound();

                        var ev = team.AddMember(command.MemberName);

                        session.Events.Append(teamAggregateId.GetStreamName(), ev);

                        await session.SaveChangesAsync(cancellationToken);

                        return Results.NoContent();
                    }
                    catch (Exception ex)
                    {
                        return Results.BadRequest(ex);
                    }
                })
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status204NoContent)
                .WithName(Name)
                .WithTags("Teams");

            return app;
        }
    }
}
