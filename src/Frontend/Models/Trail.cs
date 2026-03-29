namespace Frontend.Models;

public class Trail
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public double LengthKm { get; set; }
    public int ElevationGainM { get; set; }
    public string? ExternalUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ConditionReport> Reports { get; set; } = new();
}