using Microsoft.EntityFrameworkCore;
using TrailApi.Data;
using TrailApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<TrailDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS for Blazor frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Auto-run migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TrailDbContext>();
    db.Database.Migrate();
}

app.MapGet("/health", () => "healthy");

app.MapTrailEndpoints();
app.MapReportEndpoints();
app.Run();