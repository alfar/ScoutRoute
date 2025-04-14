namespace ScoutRoute.Routes.Domain.Events
{
    internal class ProjectUpdatedEvent : DomainEvent
    {
        public required string Name { get; init; }
    }
}
