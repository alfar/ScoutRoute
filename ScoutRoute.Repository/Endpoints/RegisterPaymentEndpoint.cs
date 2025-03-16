using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Commands;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class RegisterPaymentEndpoint
    {
        public static string Name = "RegisterPayment";

        public static IEndpointRouteBuilder MapRegisterPayment(this IEndpointRouteBuilder app)
        {
            app
                .MapPost("/", async (RegisterPaymentCommand command, IMediator mediator) =>
                {
                    await mediator.Send(command);

                    return TypedResults.Ok();
                })
                .WithName(Name);

            return app;
        }
    }
}
