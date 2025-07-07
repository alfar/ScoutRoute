using ScoutRoute.Payments.Contracts.Commands;
using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Mapping.Commands
{
    internal static class DomainMappings
    {
        public static Payment ToPayment(this RegisterPaymentCommand command)
        {
            return Payment.Create(new PaymentId(command.Id), command.Message, command.Amount, command.Received);
        }
    }
}
