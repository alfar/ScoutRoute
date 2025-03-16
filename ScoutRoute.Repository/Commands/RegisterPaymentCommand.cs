using MediatR;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Mapping.Commands;
using ScoutRoute.Payments.Repository;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Payments.Commands
{
    public class RegisterPaymentCommand : IRequest
    {
        public required PaymentId Id { get; init; }
        public string? Message { get; init; }
        public required Money Amount { get; init; }
        public required DateTimeOffset Received { get; init; }
    }

    internal class RegisterPaymentCommandHandler(IPaymentWriter paymentWriter) : IRequestHandler<RegisterPaymentCommand>
    {
        public async Task Handle(RegisterPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = request.ToPayment();
            
            await paymentWriter.SavePaymentAsync(payment);
        }
    }
}
