using System.Diagnostics;
using logly.Infrastructure;
using logly.Logging;
using logly.Models;
using Microsoft.AspNetCore.Http;

namespace logly
{
    /// <summary>
    /// Request and Response logging middleware
    /// </summary>
    public class LoglyMiddleware
    {
        internal static LoggerOptions LoggerOptions { get; set; }
        private readonly RequestDelegate _next;
        private readonly Logger _logger;

        public LoglyMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = new Logger();
        }

        /// <summary>
        /// Method invoked in the http process
        /// </summary>
        /// <param name="context">The current http context</param>
        /// <returns>void</returns>
        public async Task Invoke(HttpContext context)
        {
            // start timer for request pipeline
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            // track a reference to the response body
            var originalResponseStream = context.Response.Body;

            using (var loggableResponseStream = new MemoryStream())
            {
                context.Response.Body = loggableResponseStream;

                try
                {
                    // retrieve request details
                    var request = FormatRequest(context.Request);

                    // continue down the pipeline
                    await _next(context);

                    // stop timer
                    stopWatch.Stop();

                    // retrieve response details
                    var response = FormatResponse(context.Response, context.Response.StatusCode);
                    response.ResponseTime = stopWatch.ElapsedMilliseconds;

                    // log the request and response details
                    _logger.Log(request, response);

                    await context.Response.Body.CopyToAsync(originalResponseStream);
                }
                finally
                {
                    // reassign the original stream. necessary to write exceptions to the stream
                    context.Response.Body = originalResponseStream;
                }
            }
        }

        /// <summary>
        /// Extracts the required request data from the http request object
        /// </summary>
        /// <param name="request">The current HTTP request instance</param>
        /// <returns>A request model object with the necessary data</returns>
        private static Request FormatRequest(HttpRequest request)
        {
            return new Request
            {
                Url = $"{request.Path}{request.QueryString}",
                Method = request.Method
            };
        }

        /// <summary>
        /// Extracts the required response data from the http reponse object
        /// </summary>
        /// <param name="response">The current HTTP response</param>
        /// <param name="statusCode">The status of the response</param>
        /// <returns>A response model object with the necessary data</returns>
        private static Response FormatResponse(HttpResponse response, int statusCode)
        {
            var bodyLength = response.Body.Length;

            // reset the stream
            response.Body.Seek(0, SeekOrigin.Begin);

            // generate a response
            return new Response
            {
                StatusCode = statusCode,
                ResponseLength = bodyLength
            };
        }
    }
}