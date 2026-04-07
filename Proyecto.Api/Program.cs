using Proyecto.Api.Data;
using Proyecto.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ── Services ──────────────────────────────────────────
builder.Services.AddControllers();

// Register DbService as singleton using connection string from appsettings.json
var useInMemory = Environment.GetEnvironmentVariable("USE_INMEMORY_DB");
string connStr;
if (!string.IsNullOrEmpty(useInMemory) && useInMemory == "1")
{
    connStr = "INMEMORY";
    builder.Logging.AddConsole();
    builder.Services.AddSingleton(new DbService(connStr));
    Console.WriteLine("[WARN] Running API in INMEMORY mode (no SQL Server). Responses will be empty/defaults.");
}
else
{
    connStr = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddSingleton(new DbService(connStr));
}

// ── App pipeline ──────────────────────────────────────
var app = builder.Build();

// API Key authentication middleware (before routing)
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
