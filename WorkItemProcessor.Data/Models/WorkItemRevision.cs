

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class WorkItemRevision
{
    [Key]
    public long Id { get; set; }
    [JsonPropertyName("System.AreaPath")]
    public string AreaPath { get; set; }

    [JsonPropertyName("System.TeamProject")]
    public string TeamProject { get; set; }

    [JsonPropertyName("System.IterationPath")]
    public string IterationPath { get; set; }

    [JsonPropertyName("System.WorkItemType")]
    public string WorkItemType { get; set; }

    [JsonPropertyName("System.State")]
    public string State { get; set; }

    [JsonPropertyName("System.Reason")]
    public string Reason { get; set; }

    [JsonPropertyName("System.CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("System.CreatedBy")]
    public User CreatedBy { get; set; }

    [JsonPropertyName("System.ChangedDate")]
    public DateTime ChangedDate { get; set; }

    [JsonPropertyName("System.ChangedBy")]
    public User ChangedBy { get; set; }

    [JsonPropertyName("System.Title")]
    public string Title { get; set; }

    [JsonPropertyName("Url")]
    public string Url { get; set; }

    [JsonPropertyName("Microsoft.VSTS.Common.Severity")]
    public string Severity { get; set; }

    public WebHookEvent WebHookEvent { get; set; }
}

