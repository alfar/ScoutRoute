using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoutRoute.Routes.Contracts.ValueTypes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain.Events;
using ScoutRoute.Routes.Stops.Domain.Events;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Stops.Domain
{
    public class StopAggregate
    {
        public string Id { get; set; } = "";

        private ProjectId ProjectId { get; set; }
        private StopId StopId { get; set; }
        private ContactPerson? ContactPerson { get; set; }
        private string Title { get; set; } = "";
        private int Quantity { get; set; } = 1;

        private decimal Latitude { get; set; }
        private decimal Longitude { get; set; }
        private string Comment { get; set; } = "";

        private int Status { get; set; } = 0; // Active
        private RouteId? RouteId { get; set; }

        private bool Deleted { get; set; }

        private void EnsureNotDeleted()
        {
            if (Deleted)
                throw new InvalidOperationException($"Stop {StopId} is deleted.");
        }

        public void Apply(StopCreatedEvent @event)
        {
            ProjectId = @event.ProjectId;
            StopId = @event.StopId;
            ContactPerson = @event.ContactPerson;
            Title = @event.Title;
            Quantity = @event.Quantity;
            Latitude = @event.Latitude;
            Longitude = @event.Longitude;
            Comment = @event.Comment;
        }

        public void Apply(StopDeletedEvent @event)
        {
            if (@event.StopId == StopId)
            {
                Deleted = true;
            }
        }

        public void Apply(RouteStopAddedEvent @event)
        {
            if (@event.StopId == StopId)
            {
                RouteId = @event.RouteId;
            }
        }

        public void Apply(StopCompletedEvent @event)
        {
            if (@event.StopId == StopId)
            {
                Status = 1;
            }
        }

        public void Apply(StopNotFoundEvent @event)
        {
            if (@event.StopId == StopId)
            {
                Status = 2;
            }
        }

        public void Apply(RouteStopRemovedEvent @event)
        {
            if (@event.StopId == StopId)
            {
                RouteId = null;
            }
        }

        public static StopCreatedEvent Create(
            ProjectId projectId,
            StopId stopId,
            ContactPerson contactPerson,
            string title,
            int quantity,
            decimal latitude,
            decimal longitude,
            string comment,
            UserId userId
        )
        {
            return new StopCreatedEvent(
                projectId,
                stopId,
                contactPerson,
                title,
                quantity,
                latitude,
                longitude,
                comment,
                userId
            );
        }

        public StopCompletedEvent MarkCompleted()
        {
            EnsureNotDeleted();
            return new StopCompletedEvent(ProjectId, StopId);
        }

        public StopNotFoundEvent MarkNotFound()
        {
            EnsureNotDeleted();
            return new StopNotFoundEvent(ProjectId, StopId);
        }

        public StopDeletedEvent Delete()
        {
            EnsureNotDeleted();
            return new StopDeletedEvent(ProjectId, StopId);
        }
    }
}
