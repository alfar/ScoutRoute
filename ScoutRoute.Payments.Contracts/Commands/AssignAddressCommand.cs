namespace ScoutRoute.Payments.Contracts.Commands
{
    public class AssignAddressCommand
    {
        public required Guid AddressId { get; set; }
    }
}
