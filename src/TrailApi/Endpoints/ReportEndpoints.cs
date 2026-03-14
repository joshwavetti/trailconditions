using Microsoft.EntityFrameworkCore;
using TrailApi.Data;
using TrailApi.Models;

namespace TrailApi.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this WebApplication app)
    {
        // GET all reports for a trail
        app.MapGet("/trails/{trailId}/reports", async (Guid trailId, TrailDbContext db) =>
        {
            var reports = await db.ConditionReports
                .Include(r => r.Tags)
                .Where(r => r.TrailId == trailId)
                .OrderByDescending(r => r.ReportedAt)
                .ToListAsync();

            return Results.Ok(reports);
        });

        // POST submit a condition report
        app.MapPost("/trails/{trailId}/reports", async (Guid trailId, ConditionReport report, TrailDbContext db) =>
        {
            var trail = await db.Trails.FindAsync(trailId);
            if (trail is null) return Results.NotFound();

            report.Id = Guid.NewGuid();
            report.TrailId = trailId;
            report.ReportedAt = DateTime.UtcNow;

            db.ConditionReports.Add(report);
            await db.SaveChangesAsync();
            return Results.Created($"/trails/{trailId}/reports/{report.Id}", report);
        });

        // GET latest report for a trail (for dashboard)
        app.MapGet("/trails/{trailId}/reports/latest", async (Guid trailId, TrailDbContext db) =>
        {
            var report = await db.ConditionReports
                .Include(r => r.Tags)
                .Where(r => r.TrailId == trailId)
                .OrderByDescending(r => r.ReportedAt)
                .FirstOrDefaultAsync();

            return report is null ? Results.NotFound() : Results.Ok(report);
        });
    }
}