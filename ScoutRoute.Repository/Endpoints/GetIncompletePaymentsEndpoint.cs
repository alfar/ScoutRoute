using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Contracts.Endpoints;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Queries;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Payments.Endpoints
{
    internal static class GetIncompletePaymentsEndpoint
    {
        public static string Name => "GetIncompletePayments";

        public static IEndpointRouteBuilder MapGetIncompletePayments(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(PaymentEndpoints.GetIncompletePayments, async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetIncompletePaymentsQuery());

                    return TypedResults.Ok(result);
                })
                .WithName(Name);

            return app;
        }
    }
}
