using EventStore.Client;
using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Repository.ReadModels
{
    public class ProjectSubscriber(EventStoreClient eventStoreClient, IProjectCache projectCache)
    {
        private StreamSubscription subscription;

        public async Task Subscribe()
        {
            subscription = await eventStoreClient.SubscribeToAllAsync(FromAll.Start, async (subscription, resolvedEvent, cancellationToken) =>
            {
                switch (resolvedEvent.OriginalEvent.EventType)
                {
                    case "ProjectCreated":
                        var projectCreated = await JsonSerializer.DeserializeAsync<ProjectCreatedEvent>(new MemoryStream(resolvedEvent.OriginalEvent.Data.ToArray()));

                        if (projectCreated is not null)
                        {
                            projectCache.CacheListProject(projectCreated.ProjectId, projectCreated.Name);
                            projectCache.CacheProject(new ProjectDto() { Id = projectCreated.ProjectId.Value, Name = projectCreated.Name, OwnerIds = [projectCreated.OwnerId.Value] });
                        }
                        break;
                    case "ProjectUpdated":
                        var projectUpdated = await JsonSerializer.DeserializeAsync<ProjectUpdatedEvent>(new MemoryStream(resolvedEvent.OriginalEvent.Data.ToArray()));
                        if (projectUpdated is not null)
                        {
                            projectCache.CacheListProject(projectUpdated.ProjectId, projectUpdated.Name);

                            var project = projectCache.GetProject(projectUpdated.ProjectId)!;
                            projectCache.CacheProject(new ProjectDto() { Id = projectUpdated.ProjectId.Value, Name = projectUpdated.Name, OwnerIds = project.OwnerIds });
                        }
                        break;
                    default:
                        Console.WriteLine(resolvedEvent.OriginalEvent.EventType);
                        break;
                }
            });
        }
    }
}
