using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Teams;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Teams.Mappings;
using ScoutRoute.Routes.Teams.Projections;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Teams.Endpoints
{
    internal static class GetAllTeamsEndpoint
    {
        public const string Name = "GetAllTeams";

        public static IEndpointRouteBuilder MapGetAllTeams(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Teams.GetAllTeams, async (Guid projectId, UserId ownerId, IQuerySession session) =>
                {
                    var projId = new ProjectId(projectId);

                    var teams = await session.Query<Team>().Where(t => t.ProjectId == projId).ToListAsync();

                    return TypedResults.Ok(teams.ToListDtos());
                })
                .RequireAuthorization()
                .Produces<IEnumerable<ListTeamDto>>()
                .WithName(Name)
                .WithTags("Teams");

            return app;
        }
    }
}
