using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Repository;

namespace ScoutRoute.Routes.Endpoints.Projects
{
    internal static class CreateProjectEndpoint
    {
        public const string Name = "CreateProject";

        public static IEndpointRouteBuilder MapCreateProject(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Projects.CreateProject, async (CreateProjectCommand command, IProjectWriter writer) =>
                {
                    if (await writer.CreateProjectAsync(new ProjectId(command.Id), command.Name, new PersonId(Guid.NewGuid())))
                    {
                        return Results.NoContent();
                    }

                    return Results.BadRequest();
                })
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
