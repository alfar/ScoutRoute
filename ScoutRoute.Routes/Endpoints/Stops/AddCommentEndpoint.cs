using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Stops;

namespace ScoutRoute.Routes.Endpoints.Stops
{
    internal static class AddCommentEndpoint
    {
        public const string Name = "AddComment";

        public static IEndpointRouteBuilder MapAddComment(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(Contracts.Endpoints.Endpoints.Stops.AddComment, (AddCommentCommand command) =>
                {

                })
                .WithName(Name)
                .WithTags("Stops");
            return app;
        }
    }
}
