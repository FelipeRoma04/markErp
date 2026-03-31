using Proyecto.Api.Data;
using Proyecto.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── Services ──────────────────────────────────────────
builder.Services.AddControllers();

// Register DbService as singleton using connection string from appsettings.json
var connStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddSingleton(new DbService(connStr));

// ── App pipeline ──────────────────────────────────────
var app = builder.Build();

// API Key authentication middleware (before routing)
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
