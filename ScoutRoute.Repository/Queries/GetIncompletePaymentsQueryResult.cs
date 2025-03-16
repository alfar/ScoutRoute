using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Queries
{
    public class GetIncompletePaymentsQueryResult(IEnumerable<Payment> payments)
    {
        public IEnumerable<Payment> Payments { get; init; } = payments;
    }
}
