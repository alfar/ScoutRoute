using MediatR;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Repository;
using StackExchange.Redis;
using System.Text.Json;

namespace ScoutRoute.Payments.Commands
{
    public class AssignAddressCommand : IRequest<AssignAddressCommandResult>
    {
        public required PaymentId PaymentId { get; init; }
        public required AddressId AddressId { get; init; }
    }

    public record AssignAddressCommandResult(bool Success);

    internal class AssignAddressCommandHandler(IPaymentReader reader, IPaymentWriter writer) : IRequestHandler<AssignAddressCommand, AssignAddressCommandResult>
    {
        public async Task<AssignAddressCommandResult> Handle(AssignAddressCommand request, CancellationToken cancellationToken)
        {
            var payment = await reader.GetPaymentByIdAsync(request.PaymentId);

            if (payment is null)
                return new(false);

            payment.SetAddressId(request.AddressId);

            await writer.SavePaymentAsync(payment);

            return new(true);
        }
    }
}
