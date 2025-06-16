using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Teams.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Teams.Domain
{
    public class TeamAggregate
    {
        public string Id { get; set; } = "";

        private ProjectId ProjectId { get; set; }
        private TeamId TeamId { get; set; }
        private string Name { get; set; } = "";
        private string TeamLead { get; set; } = "";
        private string Phone { get; set; } = "";
        private TrailerType TrailerType { get; set; }
        private List<string> Members { get; set; } = new();

        public void Apply(TeamCreatedEvent @event)
        {
            ProjectId = @event.ProjectId;   
            TeamId = @event.TeamId;
        }

        public void Apply(TeamNameUpdatedEvent @event)
        {
            Name = @event.Name;
        }

        public void Apply(TeamLeadUpdatedEvent @event)
        {
            TeamLead = @event.TeamLead;
        }

        public void Apply(TeamPhoneUpdatedEvent @event)
        {
            Phone = @event.Phone;
        }

        public void Apply(TeamTrailerTypeUpdatedEvent @event)
        {
            TrailerType = @event.TrailerType;
        }

        public void Apply(TeamMemberAddedEvent @event)
        {
            Members.Add(@event.MemberName);
        }

        public void Apply(TeamMemberRemovedEvent @event)
        {
            Members.Remove(@event.MemberName);
        }

        public static TeamCreatedEvent CreateTeam(ProjectId projectId, TeamId teamId) => new TeamCreatedEvent(projectId, teamId);

        public TeamNameUpdatedEvent UpdateName(string name) => new TeamNameUpdatedEvent(ProjectId, TeamId, name);
        public TeamLeadUpdatedEvent UpdateTeamLead(string teamLead) => new TeamLeadUpdatedEvent(ProjectId, TeamId, teamLead);
        public TeamPhoneUpdatedEvent UpdatePhone(string phone) => new TeamPhoneUpdatedEvent(ProjectId, TeamId, phone);
        public TeamTrailerTypeUpdatedEvent UpdateTrailerType(TrailerType trailerType) => new TeamTrailerTypeUpdatedEvent(ProjectId, TeamId, trailerType);
        public TeamMemberAddedEvent AddMember(string memberName) => new TeamMemberAddedEvent(ProjectId, TeamId, memberName);
        public TeamMemberRemovedEvent? RemoveMember(string memberName)
        {
            if (Members.Contains(memberName))
            {
                return new TeamMemberRemovedEvent(ProjectId, TeamId, memberName);
            }

            return null;
        }
    }
}
