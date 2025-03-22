using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Contracts.Commands;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Mapping.Commands;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class RegisterPaymentEndpoint
    {
        public static string Name = "RegisterPayment";

        public static IEndpointRouteBuilder MapRegisterPayment(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(PaymentEndpoints.RegisterPayment, async (RegisterPaymentCommand command, IMediator mediator) =>
                {
                    await mediator.Send(command.ToRequest());

                    return TypedResults.Ok();
                })
                .WithName(Name);

            return app;
        }
    }
}
