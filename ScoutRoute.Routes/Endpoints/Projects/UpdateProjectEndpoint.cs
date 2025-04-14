using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Repository;

namespace ScoutRoute.Routes.Endpoints.Projects
{
    internal static class UpdateProjectEndpoint
    {
        public const string Name = "UpdateProject";

        public static IEndpointRouteBuilder MapUpdateProject(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Projects.UpdateProject, async (Guid projectId, UpdateProjectCommand command, IProjectWriter writer) =>
                {
                    if (await writer.UpdateProjectAsync(new ProjectId(projectId), command.Name))
                    {
                        return Results.NoContent();
                    }
                    return Results.NotFound();
                })
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
