using ScoutRoute.Shared.ValueTypes.MoneyAmounts;

namespace ScoutRoute.Payments.Contracts.Commands
{
    public class RegisterPaymentCommand
    {
        public required Guid Id { get; init; }
        public string? Message { get; init; }
        public required Money Amount { get; init; }
        public required DateTimeOffset Received { get; init; }
    }
}
