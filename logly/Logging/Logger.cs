using System;
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
            var consoleColorStash = Console.ForegroundColor;

            if (loggerOptions.ShowRequestMethod)
            {
                Console.Write(request.Method.ToUpperInvariant());
            }
            else
            {
                Console.Write("-");
            }

            if (loggerOptions.ShowUrl)
            {
                Console.Write(" " + request.Url);
            }
            else
            {
                Console.Write(" -");
            }

            if (loggerOptions.ShowStatusCode)
            {
                Console.ForegroundColor = response.StatusCodeColor;
                Console.Write(" " + response.StatusCode);
            }
            else
            {
                Console.Write(" -");
            }

            Console.ForegroundColor = consoleColorStash;
            if (loggerOptions.ShowResponseTime)
            {
                Console.Write(" - " + response.ResponseTime + " ms");
            }
            else
            {
                Console.Write(" -");
            }
            
            if (loggerOptions.ShowResponseLength)
            {
                Console.Write(" " + response.ResponseLength + " bytes");
            }
            else
            {
                Console.Write(" -");
            }
            
            // move to next line
            Console.WriteLine();
        }
    }
}