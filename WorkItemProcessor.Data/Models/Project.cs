using System.ComponentModel.DataAnnotations;

public class Project
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProjectSK { get; set; }
    public Organization Organization { get; set; }
}