using ScoutRoute.Payments.Domain;
using ScoutRoute.Shared.ValueTypes.MoneyAmounts;
using StackExchange.Redis;
using System.Text.Json;

namespace ScoutRoute.Payments.Repository
{
    internal class PaymentReader(IConnectionMultiplexer connectionMultiplexer) : IPaymentReader
    {
        public Task<IReadOnlyCollection<Payment>> GetIncompletePaymentsAsync()
        {
            return Task.FromResult<IReadOnlyCollection<Payment>>(
                new List<Payment>
                {
                    Payment.Create(new PaymentId(Guid.NewGuid()), "Hej! Vil I hente mit træ på Vesterbakken 28?", Money.Create(40), new DateTimeOffset(2025, 12, 22, 8, 21, 03, TimeSpan.Zero))
                }
                );
        }

        public async Task<Payment?> GetPaymentByIdAsync(PaymentId paymentId)
        {
            var db = connectionMultiplexer.GetDatabase();
            var paymentJson = await db.StringGetAsync(new RedisKey(paymentId.ToString()));

            if (!paymentJson.HasValue)
                return null;

            return JsonSerializer.Deserialize<Payment>(paymentJson!)!;
        }
    }
}
