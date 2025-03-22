using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Mapping.Commands
{
    internal static class DomainMappings
    {
        public static Payment ToPayment(this RegisterPaymentRequest command)
        {
            return Payment.Create(command.Id, command.Message, command.Amount, command.Received);
        }
    }
}
