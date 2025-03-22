using MediatR;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Mapping.Commands;
using ScoutRoute.Payments.Repository;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Payments.Commands
{
    public class RegisterPaymentRequest : IRequest
    {
        public required PaymentId Id { get; init; }
        public string? Message { get; init; }
        public required Money Amount { get; init; }
        public required DateTimeOffset Received { get; init; }
    }

    internal class RegisterPaymentHandler(IPaymentReader reader, IPaymentWriter paymentWriter) : IRequestHandler<RegisterPaymentRequest>
    {
        public async Task Handle(RegisterPaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = await reader.GetPaymentByIdAsync(request.Id);

            if (payment is not null)
            {
                // Don't reregister, it should be the same payment again
                return;
            }

            payment = request.ToPayment();
            
            await paymentWriter.SavePaymentAsync(payment);
        }
    }
}
