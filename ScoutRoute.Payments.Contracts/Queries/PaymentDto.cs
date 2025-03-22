namespace ScoutRoute.Payments.Contracts.Queries
{
    public class PaymentDto
    {
        public required Guid Id { get; init; }
        public required string? Message { get; init; }
        public required decimal Amount { get; init; }
        public required DateTimeOffset Received { get; init; }
        public required Guid? AddressId { get; init; }
        public required bool IsCompleted { get; init; }
    }
}