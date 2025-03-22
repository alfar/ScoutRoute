using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Contracts.Commands;
using ScoutRoute.Payments.Contracts.Queries;
using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Mapping.Commands
{
    internal static class ContractMappings
    {
        public static RegisterPaymentRequest ToRequest(this RegisterPaymentCommand dto)
        {
            return new RegisterPaymentRequest() { Id = new PaymentId(dto.Id), Message = dto.Message, Amount = dto.Amount, Received = dto.Received };
        }

        public static AssignAddressRequest ToRequest(this AssignAddressCommand dto, PaymentId paymentId)
        {
            return new AssignAddressRequest() { AddressId = new AddressId(dto.AddressId), PaymentId = paymentId };
        }

        public static PaymentDto ToDto(this Payment payment)
        {
            return new PaymentDto()
            {
                Id = payment.Id.Value,
                Message = payment.Message,
                Amount = payment.Amount.Value,
                Received = payment.Received,
                AddressId = payment.AddressId?.Value,
                IsCompleted = payment.IsCompleted,
            };
        }

        public static IEnumerable<PaymentDto> ToDtos(this IReadOnlyCollection<Payment> payments)
        {
            return payments.Select(p => p.ToDto());
        }
    }
}
