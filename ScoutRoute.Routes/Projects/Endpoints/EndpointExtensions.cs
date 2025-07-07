using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapProjectEndpoints(
            this IEndpointRouteBuilder endpoints
        )
        {
            endpoints
                .MapCreateProject()
                .MapDeleteProject()
                .MapGetProject()
                .MapGetAllProjects()
                .MapShareProject()
                .MapUpdateProject();

            return endpoints;
        }
    }
}
