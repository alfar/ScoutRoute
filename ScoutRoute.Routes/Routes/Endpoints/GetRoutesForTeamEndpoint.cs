using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Routes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Routes.Routes.Mappings;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class GetRoutesForTeamEndpoint
    {
        public const string Name = "GetRoutesForTeam";

        public static IEndpointRouteBuilder MapGetRoutesForTeamEndpoint(
            this IEndpointRouteBuilder app
        )
        {
            app.MapGet(
                    Contracts.Endpoints.Endpoints.Routes.GetForTeam,
                    async (
                        Guid projectId,
                        Guid teamId,
                        IQuerySession session,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        var id = new ProjectId(projectId);
                        var project = await session.LoadAsync<Project>(id, cancellationToken);

                        if (project is null)
                            return Results.NotFound();

                        var assignedTeamId = new TeamId(teamId);

                        return TypedResults.Ok(
                            (
                                await session
                                    .Query<Projections.Route>()
                                    .Where(s =>
                                        s.ProjectId == id && s.AssignedTeamId == assignedTeamId
                                    )
                                    .ToListAsync(cancellationToken)
                            ).ToListDtos()
                        );
                    }
                )
                .Produces<IEnumerable<RouteDto>>()
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
