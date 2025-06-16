using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Routes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Routes.Routes.Mappings;
using ScoutRoute.Routes.Routes.Projections;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class GetAllRoutesEndpoint
    {
        public const string Name = "GetAllRoutes";

        public static IEndpointRouteBuilder MapGetAllRoutes(this IEndpointRouteBuilder app)
        {
            app.MapGet(
                    Contracts.Endpoints.Endpoints.Routes.GetAll,
                    async (
                        Guid projectId,
                        UserId ownerId,
                        IQuerySession session,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        var id = new ProjectId(projectId);
                        var project = await session.LoadAsync<Project>(id, cancellationToken);

                        if (project is null || !project.Owners.Contains(ownerId))
                            return Results.NotFound();

                        return TypedResults.Ok(
                            (
                                await session
                                    .Query<Projections.Route>()
                                    .Where(s => s.ProjectId == id)
                                    .ToListAsync(cancellationToken)
                            ).ToListDtos()
                        );
                    }
                )
                .RequireAuthorization()
                .Produces<IEnumerable<RouteDto>>()
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
