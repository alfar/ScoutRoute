using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Payments.Domain
{
    public class Payment
    {
        private Payment(PaymentId id, string? message, Money amount, DateTimeOffset received)
        {
            Id = id;
            Message = message;
            Amount = amount;
            Received = received;
        }

        public PaymentId Id { get; private set; }
        public string? Message { get; private set; }
        public Money Amount { get; private set; }
        public DateTimeOffset Received { get; private set; }
        public AddressId? AddressId { get; private set; }
        public bool IsCompleted { get; private set; }

        public void SetAddressId(AddressId addressId) { AddressId = addressId; }
        public void Complete() { IsCompleted = true; }

        public static Payment Create(PaymentId id, string? message, Money amount, DateTimeOffset received)
        {
            return new(id, message, amount, received);
        }
    }
}
