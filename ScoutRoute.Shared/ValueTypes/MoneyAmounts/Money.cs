using System.ComponentModel;

namespace ScoutRoute.Shared.ValueTypes.MoneyAmounts
{
    [TypeConverter(typeof(MoneyConverter))]
    public record Money
    {
        private Money(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        public static Money Create(decimal value)
        {
            return new(value);
        }
    };
}
