using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Api.Middleware
{
    /// <summary>
    /// Validates X-Api-Key header against the list in appsettings.json.
    /// Returns 401 if missing or invalid.
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER = "X-Api-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            // Allow health-check endpoint without auth
            if (context.Request.Path.StartsWithSegments("/health"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "API Key faltante. Use el header X-Api-Key." });
                return;
            }

            var validKeys = config.GetSection("ApiKeys:ValidKeys").Get<List<string>>();
            if (validKeys == null || !validKeys.Contains(extractedKey.ToString()))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "API Key inválida." });
                return;
            }

            await _next(context);
        }
    }
}
