namespace TrailApi.Models;

public class ReportTag
{
    public Guid Id { get; set; }
    public Guid ConditionReportId { get; set; }
    public string Tag { get; set; } = string.Empty; // Muddy, Icy, Snowy, etc.

    // Navigation back to report
    public ConditionReport ConditionReport { get; set; } = null!;
}
