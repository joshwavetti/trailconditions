namespace TrailApi.Models;

public class ConditionReport
{
    public Guid Id { get; set; }
    public Guid TrailId { get; set; }
    public string OverallCondition { get; set; } = string.Empty; // Good, Difficult, Avoid
    public string? Notes { get; set; }
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    public string ReportedBy { get; set; } = "Anonymous";

    // Tags like "Muddy", "Icy", "Snowy" etc.
    public ICollection<ReportTag> Tags { get; set; } = new List<ReportTag>();

    // Navigation back to Trail
    public Trail Trail { get; set; } = null!;
}
