using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Projects.Mappings;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    internal static class GetAllProjectsEndpoint
    {
        public const string Name = "GetAllProjects";

        public static IEndpointRouteBuilder MapGetAllProjects(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Projects.GetAll, async (UserId ownerId, IQuerySession session) =>
                {
                    var owner = await session.LoadAsync<ProjectOwner>(ownerId);

                    if (owner is null) return TypedResults.Ok(new ProjectQueryResult() { Projects = [] });
                    return TypedResults.Ok(new ProjectQueryResult() { Projects = owner!.Projects.ToDtos() });
                })
                .RequireAuthorization()
                .Produces<ProjectQueryResult>()
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }
    }
}
