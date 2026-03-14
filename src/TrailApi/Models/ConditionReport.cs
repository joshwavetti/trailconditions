using System.Text.Json.Serialization;

namespace TrailApi.Models;

public class ConditionReport
{
    public Guid Id { get; set; }
    public Guid TrailId { get; set; }
    public string OverallCondition { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    public string ReportedBy { get; set; } = "Anonymous";
    public ICollection<ReportTag> Tags { get; set; } = new List<ReportTag>();

    [JsonIgnore]
    public Trail Trail { get; set; } = null!;
}