using System;
using logly.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace logly.Extensions
{
    /// <summary>
    /// ASP.NET Core extensions to enable logly
    /// </summary>
    public static class LoglyMiddlewareExtensions
    {
        /// <summary>
        /// Add logging using the default settings
        /// </summary>
        /// <param name="builder">The application builder instance</param>
        /// <returns>The application builder instance</returns>
        public static IApplicationBuilder UseLogly(this IApplicationBuilder builder)
        {
            return UseLogly(builder, opts =>
                opts
                    .AddRequestMethod()
                    .AddStatusCode()
                    .AddResponseTime()
            );
        }
        
        /// <summary>
        /// Add loggic using settings specified by the end user
        /// </summary>
        /// <param name="builder">The application builder instance</param>
        /// <param name="action">An  optiins builder action</param>
        /// <returns>The application builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IApplicationBuilder UseLogly(this IApplicationBuilder builder, Action<LoggerOptionsBuilder> action)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            var optionsBuilder = new LoggerOptionsBuilder();
            action(optionsBuilder);

            Logly.LoggerOptions = optionsBuilder.Build();

            return builder.UseMiddleware<Logly>();
        }
    }
}