using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Shared.Extensions
{
    public static class AppExtensions
    {
        public static IHost UseScoutRouteDefaults(this IHost app)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            BsonSerializer.RegisterSerializer(new MoneyBsonSerializer());

            return app;
        }
    }
}
