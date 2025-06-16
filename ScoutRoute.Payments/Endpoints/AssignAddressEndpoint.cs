using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Contracts.Commands;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Repository;

namespace ScoutRoute.Payments.Endpoints
{

    internal static class AssignAddressEndpoint
    {
        public static readonly string Name = "AssignAddress";

        public static IEndpointRouteBuilder MapAssignAddress(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(PaymentEndpoints.AssignAddress, async (PaymentId paymentId, AssignAddressCommand dto, IPaymentReader reader, IPaymentWriter writer) =>
                {
                    var payment = await reader.GetPaymentByIdAsync(paymentId);

                    if (payment is null)
                        return Results.NotFound(); ;

                    payment.SetAddressId(new AddressId(dto.AddressId));

                    await writer.SavePaymentAsync(payment);

                    return Results.NoContent();
                })
                .WithName(Name);

            return app;
        }
    }
}
