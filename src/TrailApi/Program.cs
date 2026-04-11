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

// Auto-run migrations on startup with retry
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TrailDbContext>();
    var retries = 10;
    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"DB not ready, retrying... ({retries} attempts left). Error: {ex.Message}");
            Thread.Sleep(3000);
        }
    }
}

app.MapGet("/health", () => "healthy");

app.MapTrailEndpoints();
app.MapReportEndpoints();
app.Run();