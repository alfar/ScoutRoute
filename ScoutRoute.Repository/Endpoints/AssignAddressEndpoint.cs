using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Mapping.Commands;

namespace ScoutRoute.Payments.Endpoints
{
    public class AssignAddressDto
    {
        public required AddressId AddressId { get; set; }
    }

    internal static class AssignAddressEndpoint
    {
        public static readonly string Name = "AssignAddress";

        public static IEndpointRouteBuilder MapAssignAddress(this IEndpointRouteBuilder app)
        {
            app.MapPatch("/{paymentId:guid}/Address", async (PaymentId paymentId, AssignAddressDto dto, IMediator mediator) =>
            {
                var result = await mediator.Send(dto.ToCommand(paymentId));

                return result.Success ? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}
