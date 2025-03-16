using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Shared.ValueTypes.MoneyAmounts
{
    public class MoneyConverter : TypeConverter
    {
        private readonly TypeConverter _innerConverter = TypeDescriptor.GetConverter(typeof(decimal));

        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return _innerConverter.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        {
            return _innerConverter.CanConvertTo(context, destinationType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is decimal d)
            {
                return Money.Create(d);
            }

            var convertedValue = _innerConverter.ConvertFrom(context, culture, value);

            if (convertedValue is decimal dvalue) {
                return Money.Create(dvalue);
            }

            return null;
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            var moneyValue = (Money)value;

            if (destinationType == typeof(string))
            {
                return moneyValue.Value.ToString();
            }

            return _innerConverter.ConvertTo(context, culture, moneyValue.Value, destinationType);
        }
    }
}
