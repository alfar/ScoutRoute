using MongoDB.Bson.Serialization;

namespace ScoutRoute.Shared.ValueTypes.MoneyAmounts
{
    public class MoneyBsonSerializer : IBsonSerializer<Money>
    {
        public Type ValueType => typeof(Money);

        public Money Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var amount = context.Reader.ReadDecimal128();

            return Money.Create((decimal)amount);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Money value)
        {
            context.Writer.WriteDecimal128(value.Value);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is Money money)
            {
                context.Writer.WriteDecimal128(money.Value);
            }
            else
                throw new NotImplementedException();
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) => Deserialize(context, args);
    }
}
