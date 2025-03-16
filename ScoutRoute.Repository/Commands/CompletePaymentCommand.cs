using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Commands
{
    public class CompletePaymentCommand
    {
        public required PaymentId Id { get; init; }
    }
}
