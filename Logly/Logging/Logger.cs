using System.Text;
using System.Text.RegularExpressions;
using Logly.Models;

namespace Logly.Logging;

/// <summary>
/// Internal logger class responsible for logging HTTP request and response details.
/// </summary>
internal class Logger
{
    /// <summary>
    /// Logs the request and response details to the console.
    /// </summary>
    /// <param name="request">The request details to log.</param>
    /// <param name="response">The response details to log.</param>
    /// <exception cref="ArgumentNullException">Thrown if the logger configuration, request, or response is null.</exception>
    public void Log(Request request, Response response)
    {
        if (LoglyMiddleware.LoggerOptions == null) 
            throw new ArgumentNullException(nameof(LoglyMiddleware.LoggerOptions), "Logger options are not configured.");

        if (request == null) 
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");

        if (response == null) 
            throw new ArgumentNullException(nameof(response), "Response cannot be null.");

        var loggerOptions = LoglyMiddleware.LoggerOptions;

        // Check if the request URL matches any of the ignored glob patterns
        if (loggerOptions.IgnoreUrlGlobs.Any(glob => MatchesGlob(request.Url, glob))) 
            return; 

        // Create the anterior data to be displayed
        var anteriorOutputBuilder = new StringBuilder();
        anteriorOutputBuilder.Append(loggerOptions.ShowRequestMethod ? request.Method.ToUpperInvariant() : "-");

        if (loggerOptions.ShowUrl)
            anteriorOutputBuilder.Append(" " + request.Url);
        else
            anteriorOutputBuilder.Append(" -");

        // Create the posterior data to be displayed
        var posteriorOutputBuilder = new StringBuilder();
        if (loggerOptions.ShowResponseTime)
            posteriorOutputBuilder.Append(" - " + response.ResponseTime + " ms");
        else
            posteriorOutputBuilder.Append(" -");

        if (loggerOptions.ShowResponseLength)
            posteriorOutputBuilder.Append(" " + response.ResponseLength + " bytes");
        else
            posteriorOutputBuilder.Append(" -");

        // Stash the foreground color
        var consoleColorStash = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(anteriorOutputBuilder.ToString());

        if (loggerOptions.ShowStatusCode)
        {
            Console.ForegroundColor = response.StatusCodeColor;
            Console.Write(" " + response.StatusCode);
        }
        else
        {
            Console.Write(" -");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(posteriorOutputBuilder.ToString());

        // Restore the foreground color
        Console.ForegroundColor = consoleColorStash;

        anteriorOutputBuilder.Clear();
        posteriorOutputBuilder.Clear();
    }

    /// <summary>
    /// Checks if a URL matches a glob pattern.
    /// </summary>
    /// <param name="url">The URL to check.</param>
    /// <param name="glob">The glob pattern to match against.</param>
    /// <returns><c>true</c> if the URL matches the glob pattern; otherwise, <c>false</c>.</returns>
    private static bool MatchesGlob(string url, string glob)
    {
        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(glob)) 
            return false;

        // Convert glob pattern to a regex pattern
        var regexPattern = "^" + Regex.Escape(glob).Replace("\\*", ".*").Replace("\\?", ".") + "$";
        return Regex.IsMatch(url, regexPattern);
    }
}