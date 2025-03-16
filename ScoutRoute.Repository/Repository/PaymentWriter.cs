using ScoutRoute.Payments.Domain;
using StackExchange.Redis;
using System.Text.Json;

namespace ScoutRoute.Payments.Repository
{
    internal class PaymentWriter(IConnectionMultiplexer connectionMultiplexer) : IPaymentWriter
    {
        public async Task AssignAddressAsync(PaymentId paymentId, AddressId addressId)
        {
            var db = connectionMultiplexer.GetDatabase();
            var paymentJson = await db.StringGetAsync(new RedisKey(paymentId.ToString()));
            if (paymentJson.HasValue)
            {
                var payment = JsonSerializer.Deserialize<Payment>(paymentJson!)!;

            }
        }

        public async Task SavePaymentAsync(Payment payment)
        {
            var db = connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(new RedisKey(payment.Id.ToString()), JsonSerializer.Serialize(payment));
        }
    }
}
