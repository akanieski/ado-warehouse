using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WebHookEvent
{
    [Key]
    public long Id { get; set; }
    public DateTime ProcessedDate { get; set; }
    public string RawData { get; set; }
}