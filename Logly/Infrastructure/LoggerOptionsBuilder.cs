namespace Logly.Infrastructure;

/// <summary>
/// Builder class for configuring <see cref="LoggerOptions"/>.
/// Provides a fluent API to enable or disable specific logging features.
/// </summary>
public class LoggerOptionsBuilder
{
    private readonly LoggerOptions _loggerOptions = new();

    /// <summary>
    /// Enables logging of the HTTP request method (e.g., GET, POST).
    /// </summary>
    /// <returns>The current instance of <see cref="LoggerOptionsBuilder"/> for method chaining.</returns>
    public LoggerOptionsBuilder AddRequestMethod()
    {
        _loggerOptions.ShowRequestMethod = true;
        return this;
    }

    /// <summary>
    /// Enables logging of the request URL.
    /// </summary>
    /// <returns>The current instance of <see cref="LoggerOptionsBuilder"/> for method chaining.</returns>
    public LoggerOptionsBuilder AddUrl()
    {
        _loggerOptions.ShowUrl = true;
        return this;
    }

    /// <summary>
    /// Enables logging of the HTTP response status code (e.g., 200, 404).
    /// </summary>
    /// <returns>The current instance of <see cref="LoggerOptionsBuilder"/> for method chaining.</returns>
    public LoggerOptionsBuilder AddStatusCode()
    {
        _loggerOptions.ShowStatusCode = true;
        return this;
    }

    /// <summary>
    /// Enables logging of the response body length (in bytes).
    /// </summary>
    /// <returns>The current instance of <see cref="LoggerOptionsBuilder"/> for method chaining.</returns>
    public LoggerOptionsBuilder AddResponseLength()
    {
        _loggerOptions.ShowResponseLength = true;
        return this;
    }

    /// <summary>
    /// Enables logging of the time taken to process the request (in milliseconds).
    /// </summary>
    /// <returns>The current instance of <see cref="LoggerOptionsBuilder"/> for method chaining.</returns>
    public LoggerOptionsBuilder AddResponseTime()
    {
        _loggerOptions.ShowResponseTime = true;
        return this;
    }

    /// <summary>
    /// Adds URL glob patterns to exclude from logging.
    /// For example, "/health" or "/api/status/*" can be used to ignore specific endpoints.
    /// </summary>
    /// <param name="globs">A list of URL glob patterns to ignore.</param>
    /// <returns>The current instance of <see cref="LoggerOptionsBuilder"/> for method chaining.</returns>
    public LoggerOptionsBuilder IgnoreUrls(params string[] globs)
    {
        _loggerOptions.IgnoreUrlGlobs.AddRange(globs);
        return this;
    }

    /// <summary>
    /// Builds and returns the configured <see cref="LoggerOptions"/> instance.
    /// </summary>
    /// <returns>A configured instance of <see cref="LoggerOptions"/>.</returns>
    public LoggerOptions Build() => _loggerOptions;
}
