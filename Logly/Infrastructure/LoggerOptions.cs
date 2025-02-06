namespace logly.Infrastructure
{
    public class LoggerOptions
    {
        /// <summary>
        /// Whether or not the request method should be logged
        /// </summary>
        public bool ShowRequestMethod { get; internal set; }

        /// <summary>
        /// Whether or not the request Url should be logged
        /// </summary>
        public bool ShowUrl { get; internal set; }

        /// <summary>
        /// Whether or not the response status code should be logged
        /// </summary>
        public bool ShowStatusCode { get; internal set; }

        /// <summary>
        /// Whether or not the request Url should be logged
        /// </summary>
        public bool ShowResponseLength { get; internal set; }

        /// <summary>
        /// Whether or not the amount of time taken for the response should be logged
        /// </summary>
        public bool ShowResponseTime { get; internal set; }
    }
}