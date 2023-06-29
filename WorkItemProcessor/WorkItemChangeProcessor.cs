using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WorkItemProcessor
{
    public class WorkItemChangeProcessor
    {
        private readonly ILogger _logger;
        private readonly WorkItemProcessorDbContext _dbContext;

        public WorkItemChangeProcessor(ILoggerFactory loggerFactory, WorkItemProcessorDbContext workItemProcessorDbContext)
        {
            _logger = loggerFactory.CreateLogger<WorkItemChangeProcessor>();
            _dbContext = workItemProcessorDbContext;
        }

        [Function("WorkItemChangeProcessor")]
        public async Task Run([QueueTrigger("workitemchanges", Connection = "")] string rawWorkItem)
        {
            _logger.LogInformation($"Processing work item change");

            var payload = JsonSerializer.Deserialize<WorkItemChangedWebHookPayload>(rawWorkItem);

            payload.Resource.Revision.Fields.Url = payload.Resource.Revision.Url;
            payload.Resource.Revision.Fields.WebHookEvent = new WebHookEvent()
            {
                RawData = rawWorkItem,
                ProcessedDate = DateTime.UtcNow
            };

            _dbContext.WorkItemRevisions.Add(payload.Resource.Revision.Fields);

            // Check if org already exists or not
            Uri uri = new Uri(payload.Resource.Revision.Url);
            string orgName = uri.AbsolutePath.TrimStart('/').Split('/').FirstOrDefault();
            var org = await _dbContext.Organizations.FirstOrDefaultAsync(p => p.UniqueId == payload.Containers.Collection.Id);
            if (org == null)
            {
                _dbContext.Organizations.Add(org = new Organization()
                {
                    Name = orgName,
                    UniqueId = payload.Containers.Collection.Id,
                    EndpointUrl = payload.Containers.Collection.BaseUrl
                });
            }
            // Check if project already exists or not
            var project = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Name == payload.Resource.Revision.Fields.TeamProject);
            if (project == null)
            {
                org.Projects.Add(new Project()
                {
                    Name = payload.Resource.Revision.Fields.TeamProject,
                    ProjectSK = payload.Containers.Project.Id
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}

public class WorkItemChangedWebHookPayload
{
    [JsonPropertyName("resource")]
    public ResourceData Resource { get; set; }

    [JsonPropertyName("resourceContainers")]
    public ResourceContainers Containers { get; set; }

    public class ResourceContainers
    {
        public ResourceContainer Project { get; set; }
        public ResourceContainer Account { get; set; }
        public ResourceContainer Collection { get; set; }
    }
    public class ResourceContainer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("baseUrl")]
        public string BaseUrl { get; set; }
    }
    public class ResourceData
    {
        [JsonPropertyName("revision")]
        public RevisionData Revision { get; set; }
    }
    public class RevisionData
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("fields")]
        public WorkItemRevision Fields { get; set; }
    }
}