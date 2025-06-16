using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    internal static class DeleteProjectEndpoint
    {
        public const string Name = "DeleteProject";

        public static IEndpointRouteBuilder MapDeleteProject(this IEndpointRouteBuilder app)
        {
            app
                .MapDelete(Contracts.Endpoints.Endpoints.Projects.DeleteProject, (Guid projectId) =>
                {

                })
                .WithName(Name)
                .WithTags("Projects");

            return app;
        }

    }
}
