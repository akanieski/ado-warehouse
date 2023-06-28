using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
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

            _dbContext.WorkItemRevisions.Add(payload.Resource.Revision.Fields);

            await _dbContext.SaveChangesAsync();
        }
    }
}

public class WorkItemChangedWebHookPayload
{
    [JsonPropertyName("resource")]
    public ResourceData Resource { get; set; }
    public class ResourceData
    {
        [JsonPropertyName("revision")]
        public RevisionData Revision { get; set; }
    }
    public class RevisionData
    {
        [JsonPropertyName("fields")]
        public WorkItemRevision Fields { get; set; }
    }
}