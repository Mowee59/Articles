using Microsoft.AspNetCore.Http;

namespace Blocks.AspNetCore.Extensions;

/// <summary>
/// Provides extension methods for ASP.NET Core HTTP request objects.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Gets the base URL (scheme + host + port) for the specified <see cref="HttpRequest"/>.
    /// </summary>
    /// <param name="req">The HTTP request to extract the base URL from.</param>
    /// <returns>
    /// The absolute base URL as a string (e.g., "https://example.com:5001/"), or <c>null</c> if <paramref name="req"/> is null.
    /// </returns>
    public static string? BaseUrl(this HttpRequest req)
    {
        // Return null if the request is null
        if (req == null)
            return null;

        // Build the Uri using scheme (http/https), host, and port (if specified)
        var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);

        // If the UriBuilder is using the default port (-1), explicitly set the port to -1 to avoid appending it to the URL
        if (uriBuilder.Uri.IsDefaultPort)
            uriBuilder.Port = -1;

        // Return the absolute URI as a string
        return uriBuilder.Uri.AbsoluteUri;
    }

    /// <summary>
    /// Retrieves the client's IP address from the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="context">The HTTP context to extract the client IP address from.</param>
    /// <returns>
    /// The client's IP address as a string. Checks the <c>X-Forwarded-For</c> header first for proxy scenarios;
    /// if that header is not present, falls back to <see cref="Connection.RemoteIpAddress"/>.
    /// Returns "Unknown" if the IP address cannot be determined.
    /// </returns>
    public static string GetClientIpAddress(this HttpContext context)
    {
        var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(forwardedFor))
        {
            // In case of multiple addresses, take the first one
            return forwardedFor.Split(',')[0].Trim();
        }

        return context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }

}
