using JasperFx.Core;
using Marten.Events.Projections;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Teams.Domain;
using ScoutRoute.Routes.Teams.Domain.Events;
using System.Collections.Immutable;

namespace ScoutRoute.Routes.Teams.Projections
{
    public sealed record Team(TeamId Id, ProjectId ProjectId, string? Name, string? TeamLead, string? Phone, TrailerType? TrailerType, ImmutableArray<string> Members);

    public class TeamProjection : EventProjection
    {
        public TeamProjection()
        {
            Project<TeamCreatedEvent>((e, ops) =>
            {
                ops.Store(new Team(e.TeamId, e.ProjectId, null, null, null, null, []));
            });

            ProjectAsync<TeamNameUpdatedEvent>(async (e, ops) =>
            {
                var team = await ops.LoadAsync<Team>(e.TeamId);
                if (team is not null)
                {
                    ops.Store(team with { Name = e.Name });
                }
            });

            ProjectAsync<TeamLeadUpdatedEvent>(async (e, ops) =>
            {
                var team = await ops.LoadAsync<Team>(e.TeamId);
                if (team is not null)
                {
                    ops.Store(team with { TeamLead = e.TeamLead });
                }
            });

            ProjectAsync<TeamPhoneUpdatedEvent>(async (e, ops) =>
            {
                var team = await ops.LoadAsync<Team>(e.TeamId);
                if (team is not null)
                {
                    ops.Store(team with { Phone = e.Phone });
                }
            });

            ProjectAsync<TeamTrailerTypeUpdatedEvent>(async (e, ops) =>
            {
                var team = await ops.LoadAsync<Team>(e.TeamId);
                if (team is not null)
                {
                    ops.Store(team with { TrailerType = e.TrailerType });
                }
            });

            ProjectAsync((Func<TeamMemberAddedEvent, Marten.IDocumentOperations, Task>)(async (e, ops) =>
            {
                var team = await ops.LoadAsync<Team>(e.TeamId);
                if (team is not null)
                {
                    Team entity = team with { Members = [.. team.Members, e.MemberName] };
                    ops.Store(entity);
                }
            }));

            ProjectAsync<TeamMemberRemovedEvent>(async (e, ops) =>
            {
                var team = await ops.LoadAsync<Team>(e.TeamId);
                if (team is not null)
                {
                    var index = team.Members.GetFirstIndex(m => m == e.MemberName);
                    ops.Store(team with { Members = [.. team.Members.Where((n, i) => i != index)] });
                }
            });
        }
    }
}
