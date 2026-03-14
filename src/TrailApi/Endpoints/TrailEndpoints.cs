using Microsoft.EntityFrameworkCore;
using TrailApi.Data;
using TrailApi.Models;

namespace TrailApi.Endpoints;

public static class TrailEndpoints
{
    public static void MapTrailEndpoints(this WebApplication app)
    {
        // GET all trails
        app.MapGet("/trails", async (TrailDbContext db) =>
            await db.Trails.ToListAsync());

        // GET single trail with its reports
        app.MapGet("/trails/{id}", async (Guid id, TrailDbContext db) =>
        {
            var trail = await db.Trails
                .Include(t => t.Reports)
                .ThenInclude(r => r.Tags)
                .FirstOrDefaultAsync(t => t.Id == id);

            return trail is null ? Results.NotFound() : Results.Ok(trail);
        });

        // POST create a trail
        app.MapPost("/trails", async (Trail trail, TrailDbContext db) =>
        {
            trail.Id = Guid.NewGuid();
            trail.CreatedAt = DateTime.UtcNow;
            db.Trails.Add(trail);
            await db.SaveChangesAsync();
            return Results.Created($"/trails/{trail.Id}", trail);
        });

        // DELETE a trail
        app.MapDelete("/trails/{id}", async (Guid id, TrailDbContext db) =>
        {
            var trail = await db.Trails.FindAsync(id);
            if (trail is null) return Results.NotFound();
            db.Trails.Remove(trail);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}