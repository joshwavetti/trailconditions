using System.Text.Json.Serialization;

namespace TrailApi.Models;

public class ReportTag
{
    public Guid Id { get; set; }
    public Guid ConditionReportId { get; set; }
    public string Tag { get; set; } = string.Empty;

    [JsonIgnore]
    public ConditionReport ConditionReport { get; set; } = null!;
}