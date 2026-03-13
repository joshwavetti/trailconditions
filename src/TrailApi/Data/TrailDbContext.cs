using Microsoft.EntityFrameworkCore;
using TrailApi.Models;

namespace TrailApi.Data;

public class TrailDbContext : DbContext
{
    public TrailDbContext(DbContextOptions<TrailDbContext> options) : base(options) { }

    public DbSet<Trail> Trails { get; set; }
    public DbSet<ConditionReport> ConditionReports { get; set; }
    public DbSet<ReportTag> ReportTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Trail
        modelBuilder.Entity<Trail>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Name).IsRequired().HasMaxLength(200);
            e.Property(t => t.Region).IsRequired().HasMaxLength(200);
            e.Property(t => t.Difficulty).IsRequired().HasMaxLength(20);
        });

        // ConditionReport
        modelBuilder.Entity<ConditionReport>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.OverallCondition).IsRequired().HasMaxLength(20);
            e.Property(r => r.ReportedBy).HasMaxLength(100);
            e.HasOne(r => r.Trail)
             .WithMany(t => t.Reports)
             .HasForeignKey(r => r.TrailId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ReportTag
        modelBuilder.Entity<ReportTag>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Tag).IsRequired().HasMaxLength(50);
            e.HasOne(t => t.ConditionReport)
             .WithMany(r => r.Tags)
             .HasForeignKey(t => t.ConditionReportId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
