using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Mapping.Commands;
using ScoutRoute.Payments.Contracts.Commands;
using ScoutRoute.Payments.Contracts.Endpoints;

namespace ScoutRoute.Payments.Endpoints
{

    internal static class AssignAddressEndpoint
    {
        public static readonly string Name = "AssignAddress";

        public static IEndpointRouteBuilder MapAssignAddress(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(PaymentEndpoints.AssignAddress, async (PaymentId paymentId, AssignAddressCommand dto, IMediator mediator) =>
                {
                    var result = await mediator.Send(dto.ToRequest(paymentId));

                    return result.Success ? Results.NoContent() : Results.NotFound();
                })
                .WithName(Name);

            return app;
        }
    }
}
