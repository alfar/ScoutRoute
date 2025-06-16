using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Repository;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class CompletePaymentEndpoint
    {
        public static readonly string Name = "CompletePayment";

        public static IEndpointRouteBuilder MapCompletePayment(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(PaymentEndpoints.CompletePayment, async (PaymentId paymentId, IPaymentReader reader, IPaymentWriter writer) =>
                {
                    var payment = await reader.GetPaymentByIdAsync(paymentId);

                    if (payment is null)
                        return Results.NotFound();

                    payment.Complete();

                    await writer.SavePaymentAsync(payment);

                    return Results.NoContent();
                })
                .WithName(Name);

            return app;
        }
    }
}
