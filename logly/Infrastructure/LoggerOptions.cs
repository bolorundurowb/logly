namespace logly.Infrastructure
{
    public class LoggerOptions
    {
        /// <summary>
        /// Whether or not the request method should be logged
        /// </summary>
        public bool ShowMethod { get; set; }

        /// <summary>
        /// Whether or not the request Url should be logged
        /// </summary>
        public bool ShowUrl { get; set; }

        /// <summary>
        /// Whether or not the response status code should be logged
        /// </summary>
        public bool ShowStatusCode { get; set; }

        /// <summary>
        /// Whether or not the request Url should be logged
        /// </summary>
        public bool ShowResponseLength { get; set; }

        /// <summary>
        /// Whether or not the amount of time taken for the response should be logged
        /// </summary>
        public bool ShowResponseTime { get; set; }
    }
}