using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;

namespace ScoutRoute.Routes.Stops.Endpoints
{
    internal static class AddCommentEndpoint
    {
        public const string Name = "AddComment";

        public static IEndpointRouteBuilder MapAddComment(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Stops.AddComment, (Guid projectId, Guid stopId, AddCommentCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
