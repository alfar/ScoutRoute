using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Endpoints;
using ScoutRoute.Payments.Queries;
using ScoutRoute.Payments.Repository;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Payments.Extensions
{
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
        {
            app
                .MapGroup("/Payments")
                    .MapAssignAddress()
                    .MapRegisterPayment()
                    .MapGetIncompletePayments();

            return app;
        }
    }
}
