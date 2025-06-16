using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Endpoints;

namespace ScoutRoute.Payments.Extensions
{
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
        {
            app
                .MapAssignAddress()
                .MapRegisterPayment()
                .MapCompletePayment()
                .MapGetIncompletePayments();

            return app;
        }
    }
}
