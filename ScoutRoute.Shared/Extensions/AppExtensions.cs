using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Shared.Extensions
{
    public static class AppExtensions
    {
        public static IHost UseScoutRouteDefaults(this IHost app)
        {
            BsonSerializer.RegisterSerializer<Money>(new MoneyBsonSerializer());

            return app;
        }
    }
}
