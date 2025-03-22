using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class CompletePaymentEndpoint
    {
        public static readonly string Name = "CompletePayment";

        public static IEndpointRouteBuilder MapCompletePayment(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(PaymentEndpoints.CompletePayment, async (PaymentId paymentId, IMediator mediator) =>
                {
                    var result = await mediator.Send(CompletePaymentRequest.Create(paymentId));

                    return result.Success ? Results.NoContent() : Results.NotFound();
                })
                .WithName(Name);

            return app;
        }
    }
}
