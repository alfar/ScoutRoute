using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Stops;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Routes.Stops.Mappings;
using ScoutRoute.Routes.Stops.Projections;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class GetUnassignedStopsEndpoint
    {
        public const string Name = "GetUnassignedStops";

        public static IEndpointRouteBuilder MapGetUnassignedStops(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Stops.GetUnassigned, async (Guid projectId, IQuerySession session, UserId userId, CancellationToken cancellationToken) =>
                {
                    var id = new ProjectId(projectId);
                    var project = await session.LoadAsync<Project>(id, cancellationToken);

                    if (project is null || !project.Owners.Contains(userId)) return Results.NotFound();

                    return TypedResults.Ok((await session.Query<Stop>().Where(s => s.ProjectId == id && s.RouteId == null).ToListAsync(cancellationToken)).ToDtos());
                })
                .Produces<IEnumerable<StopDto>>()
                .WithName(Name)
                .WithTags("Stops");

            return app;
        }
    }
}
