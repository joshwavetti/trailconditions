namespace Frontend.Models;

public class ReportTag
{
    public Guid Id { get; set; }
    public Guid ConditionReportId { get; set; }
    public string Tag { get; set; } = string.Empty;
}