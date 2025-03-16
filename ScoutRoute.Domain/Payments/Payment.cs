using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Domain.Payments
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

        public void SetMessage(string message) { this.Message = message; }

        public static Payment Create(PaymentId id, string? message, Money amount, DateTimeOffset received)
        {
            return new(id, message, amount, received);
        }
    }

    public record PaymentId(Guid Value);
    public record AddressId(Guid Value);

    public record Money
    {        
        private Money(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        public static Money Create(decimal value)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(value));
            return new(value);
        }
    };
}
