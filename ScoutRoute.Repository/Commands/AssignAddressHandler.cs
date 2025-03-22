using MediatR;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Repository;

namespace ScoutRoute.Payments.Commands
{
    public class AssignAddressRequest : IRequest<AssignAddressResult>
    {
        public required PaymentId PaymentId { get; init; }
        public required AddressId AddressId { get; init; }
    }

    public record AssignAddressResult(bool Success);

    internal class AssignAddressHandler(IPaymentReader reader, IPaymentWriter writer) : IRequestHandler<AssignAddressRequest, AssignAddressResult>
    {
        public async Task<AssignAddressResult> Handle(AssignAddressRequest request, CancellationToken cancellationToken)
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
