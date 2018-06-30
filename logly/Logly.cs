using System.Diagnostics;
using System.Threading.Tasks;
using logly.Infrastructure;
using logly.Logging;
using logly.Models;
using Microsoft.AspNetCore.Http;

namespace logly
{
    /// <summary>
    /// Request and Response logging middleware
    /// </summary>
    public class Logly
    {
        internal static LoggerOptions LoggerOptions { get; set; }
        private readonly RequestDelegate _next;
        private readonly Logger _logger;

        public Logly(RequestDelegate next)
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
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            // retrieve request details
            var request = FormatRequest(context.Request);

            await _next(context);

            // stop watch
            stopWatch.Stop();

            // retrieve  response details
            var response = FormatResponse(context.Response);
            response.ResponseTime = stopWatch.ElapsedMilliseconds;
            
            // log the data
            _logger.Log(request, response);
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
                Url = request.Path,
                Method = request.Method
            };
        }

        /// <summary>
        /// Extracts the required response data from the http reponse object
        /// </summary>
        /// <param name="response">The current HTTP response instance</param>
        /// <returns>A response model object with the necessary data</returns>
        private static Response FormatResponse(HttpResponse response)
        {
            return new Response
            {
                StatusCode = response.StatusCode,
                ResponseLength = response.Body.Length
            };
        }
    }
}