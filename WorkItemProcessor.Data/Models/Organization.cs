using System.ComponentModel.DataAnnotations;

public class Organization
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string UniqueId { get; set; }
    public string EndpointUrl { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
