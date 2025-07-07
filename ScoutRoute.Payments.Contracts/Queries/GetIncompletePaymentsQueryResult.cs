namespace ScoutRoute.Payments.Contracts.Queries
{
    public class GetIncompletePaymentsQueryResult(IEnumerable<PaymentDto> payments)
    {
        public IEnumerable<PaymentDto> Payments { get; init; } = payments;
    }
}
