using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Domain;
using ScoutRoute.Shared.ValueTypes;
using System.Security.Claims;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    internal static class UpdateProjectEndpoint
    {
        public const string Name = "UpdateProject";

        public static IEndpointRouteBuilder MapUpdateProject(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Projects.UpdateProject, async (Guid projectId, UpdateProjectCommand command, IQuerySession query, IDocumentStore store, ClaimsPrincipal user, CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var ownerId = user.FindFirstValue(ClaimTypes.NameIdentifier);

                        if (string.IsNullOrEmpty(ownerId))
                        {
                            return Results.Unauthorized();
                        }

                        var session = await store.LightweightSerializableSessionAsync(cancellationToken);

                        var id = new ProjectId(projectId);

                        var stream = await session.Events.FetchForWriting<ProjectAggregate>(id.GetStreamName(), cancellationToken);

                        var project = stream.Aggregate;

                        if (project is null) return Results.NotFound();

                        var ev = project.UpdateName(command.Name, UserId.Parse(ownerId));

                        session.Events.Append(id.GetStreamName(), ev);

                        await session.SaveChangesAsync(cancellationToken);
                        return Results.NoContent();
                    }
                    catch (Exception ex)
                    {
                        return Results.BadRequest(ex.Message);
                    }
                })
                .RequireAuthorization()
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent)
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
