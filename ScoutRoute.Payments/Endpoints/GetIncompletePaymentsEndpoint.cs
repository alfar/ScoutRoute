using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Mapping.Commands;
using ScoutRoute.Payments.Repository;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class GetIncompletePaymentsEndpoint
    {
        public static string Name => "GetIncompletePayments";

        public static IEndpointRouteBuilder MapGetIncompletePayments(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(PaymentEndpoints.GetIncompletePayments, async (IPaymentReader reader) =>
                {
                    var result = await reader.GetIncompletePaymentsAsync();

                    return TypedResults.Ok(result.ToDtos());
                })
                .WithName(Name);

            return app;
        }
    }
}
