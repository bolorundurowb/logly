namespace Logly.Infrastructure;

/// <summary>
/// Configuration options for the logging middleware.
/// Determines which details of the HTTP request and response are logged.
/// </summary>
public class LoggerOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the HTTP request method (e.g., GET, POST) should be logged.
    /// </summary>
    public bool ShowRequestMethod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the request URL should be logged.
    /// </summary>
    public bool ShowUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the HTTP response status code (e.g., 200, 404) should be logged.
    /// </summary>
    public bool ShowStatusCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the length of the response body (in bytes) should be logged.
    /// </summary>
    public bool ShowResponseLength { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the time taken to process the request (in milliseconds) should be logged.
    /// </summary>
    public bool ShowResponseTime { get; set; }

    /// <summary>
    /// Gets or sets a list of URL glob patterns to exclude from logging.
    /// For example, "/health" or "/api/status/*" can be used to ignore specific endpoints.
    /// Default is an empty list.
    /// </summary>
    public List<string> IgnoreUrlGlobs { get; set; } = new();
}
