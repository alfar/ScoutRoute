using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Mappings;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Shared.ValueTypes;
using System.Security.Claims;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    internal static class GetProjectEndpoint
    {
        public const string Name = "GetProject";

        public static IEndpointRouteBuilder MapGetProject(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Projects.Get, async (Guid projectId, IQuerySession session, ClaimsPrincipal user, CancellationToken cancellationToken) =>
                {
                    var ownerId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (string.IsNullOrEmpty(ownerId))
                    {
                        return Results.Unauthorized();
                    }

                    var id = new ProjectId(projectId);

                    var project = await session.LoadAsync<Project>(id, cancellationToken);

                    if (project is null || !project.Owners.Contains(UserId.Parse(ownerId)))
                    {
                        return Results.NotFound();
                    }

                    return TypedResults.Ok(project.ToDto());
                })
                .RequireAuthorization()
                .Produces<ProjectDto>()
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
