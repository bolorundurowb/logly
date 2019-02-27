using System;
using System.Text;
using logly.Models;

namespace logly.Logging
{
    internal class Logger
    {
        /// <summary>
        /// Log the response and request to the console
        /// </summary>
        /// <param name="request">The request details</param>
        /// <param name="response">The response details</param>
        /// <exception cref="ArgumentNullException">If the logger config, request or response is null</exception>
        public void Log(Request request, Response response)
        {
            if (Logly.LoggerOptions == null)
            {
                throw new ArgumentNullException(nameof(Logly.LoggerOptions));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var loggerOptions = Logly.LoggerOptions;

            // create the anterior data to be displayed
            var anteriorOutputBuilder = new StringBuilder();
            if (loggerOptions.ShowRequestMethod)
            {
                anteriorOutputBuilder.Append(request.Method.ToUpperInvariant());
            }
            else
            {
                anteriorOutputBuilder.Append("-");
            }

            if (loggerOptions.ShowUrl)
            {
                anteriorOutputBuilder.Append(" " + request.Url);
            }
            else
            {
                anteriorOutputBuilder.Append(" -");
            }

            // create the posterior data to be displayed
            var posteriorOutputBuilder = new StringBuilder();
            if (loggerOptions.ShowResponseTime)
            {
                posteriorOutputBuilder.Append(" - " + response.ResponseTime + " ms");
            }
            else
            {
                posteriorOutputBuilder.Append(" -");
            }

            if (loggerOptions.ShowResponseLength)
            {
                posteriorOutputBuilder.Append(" " + response.ResponseLength + " bytes");
            }
            else
            {
                posteriorOutputBuilder.Append(" -");
            }
            
            // stash the foreground colour
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
            
            // restore the foreground colour
            Console.ForegroundColor = consoleColorStash;

            anteriorOutputBuilder.Clear();
            posteriorOutputBuilder.Clear();
        }
    }
}