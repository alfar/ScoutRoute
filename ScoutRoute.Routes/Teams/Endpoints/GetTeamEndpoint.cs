using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Teams;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Teams.Mappings;
using ScoutRoute.Routes.Routes.Mappings;
using ScoutRoute.Routes.Teams.Projections;

namespace ScoutRoute.Routes.Teams.Endpoints
{
    public static class GetTeamEndpoint
    {
        public const string Name = "GetTeam";

        public static IEndpointRouteBuilder MapGetTeam(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Teams.GetTeam, async (Guid projectId, Guid teamId, IQuerySession session, CancellationToken cancellationToken) =>
                {
                    var pId = new ProjectId(projectId);
                    var tId = new TeamId(teamId);

                    var team = await session.LoadAsync<Team>(tId, cancellationToken);

                    if (team is null) return Results.NotFound();

                    return TypedResults.Ok(team.ToDto((await session.Query<Routes.Projections.Route>().Where(s => s.ProjectId == pId && s.AssignedTeamId == tId).ToListAsync(cancellationToken))));
                })
                .Produces<TeamDto>()
                .WithName(Name)
                .WithTags("Teams");

            return app;
        }
    }
}
