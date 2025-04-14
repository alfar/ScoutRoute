using ScoutRoute.Routes.Contracts.ValueTypes;

namespace ScoutRoute.Routes.Contracts.Commands.Stops
{
    public class CreateStopCommand
    {
        public required Guid Id { get; init; }
        public required ContactPerson ContactPerson { get; init; }
        public required string Title { get; init; }
        public required int Quantity { get; init; }
        public required string Comment { get; init; }
        public required Guid AddressId { get; init; }
    }
}
