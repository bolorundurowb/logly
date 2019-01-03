﻿using System;
using System.Diagnostics;
using System.IO;
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
            
            using (var loggableResponseStream = new MemoryStream())
            {
                var originalResponseStream = context.Response.Body;
                context.Response.Body = loggableResponseStream;

                try
                {
                    // retrieve request details
                    var request = FormatRequest(context.Request);

                    await _next(context);

                    // stop watch
                    stopWatch.Stop();

                    // retrieve  response details
                    var response = FormatResponse(loggableResponseStream, context.Response.StatusCode);
                    response.ResponseTime = stopWatch.ElapsedMilliseconds;
            
                    // log the data
                    _logger.Log(request, response);
                    
                    //reset the stream position to 0
                    loggableResponseStream.Seek(0, SeekOrigin.Begin);
                    await loggableResponseStream.CopyToAsync(originalResponseStream);
                }
                finally
                {
                    //Reassign the original stream. If we are re-throwing an exception this is important as the exception handling middleware will need to write to the response stream.
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
        /// <param name="loggableResponseStream">The current HTTP response instance stream</param>
        /// <param name="statusCode">The status of the response</param>
        /// <returns>A response model object with the necessary data</returns>
        private static Response FormatResponse(Stream loggableResponseStream, int statusCode)
        {
            return new Response
            {
                StatusCode = statusCode,
                ResponseLength = loggableResponseStream.Length
            };
        }
    }
}