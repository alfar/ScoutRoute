using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Mapping.Queries;
using ScoutRoute.Routes.Repository;

namespace ScoutRoute.Routes.Endpoints.Projects
{
    internal static class GetAllProjectsEndpoint
    {
        public const string Name = "GetAllProjects";

        public static IEndpointRouteBuilder MapGetAllProjects(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Projects.GetAll, async (IProjectReader reader) =>
                {
                    return TypedResults.Ok(new Contracts.Queries.Projects.ProjectQueryResult() { Projects = await reader.GetAllProjects() });
                })
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
