using MongoDB.Driver;
using ScoutRoute.Payments.Domain;
using StackExchange.Redis;
using System.Text.Json;

namespace ScoutRoute.Payments.Repository
{
    internal class PaymentWriter(IMongoClient mongo) : IPaymentWriter
    {
        public async Task SavePaymentAsync(Payment payment)
        {
            var db = mongo.GetDatabase("payments");

            var collection = db.GetCollection<Payment>("payments");
            await collection.ReplaceOneAsync(Builders<Payment>.Filter.Eq(p => p.Id, payment.Id), payment, new ReplaceOptions() { IsUpsert = true });
        }
    }
}
