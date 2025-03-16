using ScoutRoute.Shared.ValueTypes.MoneyAmounts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScoutRoute.ApiService.JsonConverters
{
    public class MoneyJsonConverter : JsonConverter<Money>
    {
        public override Money? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
                return default;

            var value = reader.GetDecimal();

            return Money.Create(value);
        }

        public override void Write(Utf8JsonWriter writer, Money value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
