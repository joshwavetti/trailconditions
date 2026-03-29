namespace Frontend.Models;

public class ConditionReport
{
    public Guid Id { get; set; }
    public Guid TrailId { get; set; }
    public string OverallCondition { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime ReportedAt { get; set; }
    public string ReportedBy { get; set; } = string.Empty;
    public List<ReportTag> Tags { get; set; } = new();
}