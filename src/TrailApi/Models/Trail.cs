namespace TrailApi.Models;

public class Trail
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty; // Easy, Medium, Hard
    public double LengthKm { get; set; }
    public int ElevationGainM { get; set; }
    public string? ExternalUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property - one trail has many reports
    public ICollection<ConditionReport> Reports { get; set; } = new List<ConditionReport>();
}
