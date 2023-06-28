

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User
{
    [Key]
    public long Id { get; set; }
    
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("id")]
    public string UniqueId { get; set; }

    [JsonPropertyName("uniqueName")]
    public string UniqueName { get; set; }

    [JsonPropertyName("descriptor")]
    public string Descriptor { get; set; }
}

