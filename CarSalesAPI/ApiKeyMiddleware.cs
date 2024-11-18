using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyHeaderName = "X-Api-Key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        // Engedélyezzük a Swagger UI és az OpenAPI kérések hozzáférését API kulcs nélkül
        var path = context.Request.Path.Value;
        if (path.StartsWith("/swagger") || path.StartsWith("/swagger/index.html") || path.StartsWith("/v1/swagger.json"))
        {
            await _next(context);
            return;
        }

        // API kulcs ellenőrzése
        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API kulcs hiányzik.");
            return;
        }

        var apiKey = configuration.GetValue<string>("ApiSettings:ApiKey");

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Érvénytelen API kulcs.");
            return;
        }

        await _next(context);
    }

}
