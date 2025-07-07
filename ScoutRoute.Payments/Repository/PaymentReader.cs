using MongoDB.Driver;
using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Repository
{
    internal class PaymentReader(IMongoClient mongo) : IPaymentReader
    {
        public async Task<IReadOnlyCollection<Payment>> GetIncompletePaymentsAsync()
        {
            var db = mongo.GetDatabase("payments");

            var collection = db.GetCollection<Payment>("payments");

            var result = await collection.FindAsync(Builders<Payment>.Filter.Eq(p => p.IsCompleted, false));

            return result.ToList();
        }

        public async Task<Payment?> GetPaymentByIdAsync(PaymentId paymentId)
        {
            var db = mongo.GetDatabase("payments");
            var collection = db.GetCollection<Payment>("payments");

            return (await collection.FindAsync(Builders<Payment>.Filter.Eq(p => p.Id, paymentId))).FirstOrDefault();
        }
    }
}
