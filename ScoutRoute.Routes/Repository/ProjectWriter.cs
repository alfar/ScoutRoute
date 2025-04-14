using EventStore.Client;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Domain.Events;
using System.Text.Json;

namespace ScoutRoute.Routes.Repository
{
    internal class ProjectWriter(EventStoreClient eventStoreClient) : IProjectWriter
    {
        public async Task<bool> CreateProjectAsync(ProjectId id, string name, PersonId ownerId)
        {
            var ev = ProjectAggregate.Create(id, name, ownerId);

            await eventStoreClient.AppendToStreamAsync(ev.GetStreamName(), StreamState.NoStream, [new EventData(Uuid.NewUuid(), "ProjectCreated", JsonSerializer.SerializeToUtf8Bytes(ev))]);

            return true;
        }

        public Task<bool> UpdateProjectAsync(ProjectId projectId, string name)
        {
            
        }
    }
}
