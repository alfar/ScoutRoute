using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Contracts.Commands;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Mapping.Commands;
using ScoutRoute.Payments.Repository;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class RegisterPaymentEndpoint
    {
        public static string Name = "RegisterPayment";

        public static IEndpointRouteBuilder MapRegisterPayment(this IEndpointRouteBuilder app)
        {
            app
                .MapPost(PaymentEndpoints.RegisterPayment, async (RegisterPaymentCommand command, IPaymentReader reader, IPaymentWriter writer) =>
                {
                    var payment = await reader.GetPaymentByIdAsync(new PaymentId(command.Id));

                    if (payment is not null)
                    {
                        // Don't reregister, it should be the same payment again
                        return Results.Ok();
                    }

                    payment = command.ToPayment();

                    await writer.SavePaymentAsync(payment);

                    return TypedResults.Ok();
                })
                .WithName(Name);

            return app;
        }
    }
}
