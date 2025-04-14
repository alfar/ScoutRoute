using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Endpoints.Projects
{
    internal static class GetRouteEndpoint
    {
        public const string Name = "GetRoute";

        public static IEndpointRouteBuilder MapGetRoute(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Routes.Get, (Guid projectId, Guid routeId) =>
                {

                })
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
