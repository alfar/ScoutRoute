using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Extensions;
using ScoutRoute.Routes.Projects.Domain;
using System.Security.Claims;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    internal static class CreateProjectEndpoint
    {
        public const string Name = "CreateProject";

        public static IEndpointRouteBuilder MapCreateProject(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Projects.CreateProject, async (CreateProjectCommand command, IDocumentStore store, ClaimsPrincipal user, CancellationToken cancellationToken) =>
                {
                    return await user.LoggedIn(async ownerId =>
                    {
                        try
                        {
                            var ev = ProjectAggregate.CreateProject(new ProjectId(command.Id), command.Name, ownerId);

                            using var session = await store.LightweightSerializableSessionAsync(cancellationToken);

                            session.Events.StartStream(ev.ProjectId.GetStreamName(), ev);

                            await session.SaveChangesAsync(cancellationToken);

                            return Results.NoContent();
                        }
                        catch (Exception ex)
                        {
                            return Results.BadRequest(ex);
                        }
                    });
                })
                .RequireAuthorization()
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status204NoContent)
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
