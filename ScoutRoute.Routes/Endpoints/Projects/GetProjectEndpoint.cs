using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Mapping.Queries;
using ScoutRoute.Routes.Repository;

namespace ScoutRoute.Routes.Endpoints.Projects
{
    internal static class GetProjectEndpoint
    {
        public const string Name = "GetProject";

        public static IEndpointRouteBuilder MapGetProject(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Projects.Get, async (Guid projectId, IProjectReader reader) =>
                {
                    var project = await reader.GetProjectById(new ProjectId(projectId));

                    if (project is null)
                    {
                        return Results.NotFound();
                    }

                    return TypedResults.Ok(project);
                })
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
