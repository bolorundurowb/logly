using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using logly.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace logly
{
    public class Logly
    {
        internal static LoggerOptions LoggerOptions { get; set; }
        private readonly RequestDelegate _next;
        private readonly ILogger<Logly> _logger;

        public Logly(RequestDelegate next, ILogger<Logly> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var loggableResponseStream = new MemoryStream())
            {
                var originalResponseStream = context.Response.Body;
                context.Response.Body = loggableResponseStream;

                try
                {
                    // Log request
                    _logger.LogInformation(await FormatRequest(context.Request));

                    await _next(context);

                    // Log response
                    _logger.LogInformation(await FormatResponse(loggableResponseStream, context.Response.StatusCode));
                    //reset the stream position to 0
                    loggableResponseStream.Seek(0, SeekOrigin.Begin);
                    await loggableResponseStream.CopyToAsync(originalResponseStream);
                }
                catch (Exception ex)
                {
                    // Log error
                    _logger.LogError(ex, ex.Message);

                    //allows exception handling middleware to deal with things
                    throw;
                }
                finally
                {
                    //Reassign the original stream. If we are re-throwing an exception this is important as the exception handling middleware will need to write to the response stream.
                    context.Response.Body = originalResponseStream;
                }
            }
        }

        private static async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            var messageObjToLog = new
            {
                scheme = request.Scheme,
                host = request.Host,
                path = request.Path,
                queryString = request.Query,
                requestBody = bodyAsText
            };

            return JsonConvert.SerializeObject(messageObjToLog);
        }

        private static async Task<string> FormatResponse(Stream loggableResponseStream, int statusCode)
        {
            var buffer = new byte[loggableResponseStream.Length];
            await loggableResponseStream.ReadAsync(buffer, 0, buffer.Length);

            var messageObjectToLog = new {responseBody = Encoding.UTF8.GetString(buffer), statusCode = statusCode};

            return JsonConvert.SerializeObject(messageObjectToLog);
        }
    }
}