namespace logly.Infrastructure
{
    public class LoggerOptionsBuilder
    {
        private readonly LoggerOptions _loggerOptions = new LoggerOptions();

        /// <summary>
        /// Enable request method logging
        /// </summary>
        /// <returns>The current options builder</returns>
        public LoggerOptionsBuilder AddRequestMethod()
        {
            _loggerOptions.ShowRequestMethod = true;
            return this;
        }

        /// <summary>
        /// Enable request url logging
        /// </summary>
        /// <returns>The current options builder</returns>
        public LoggerOptionsBuilder AddUrl()
        {
            _loggerOptions.ShowUrl = true;
            return this;
        }

        /// <summary>
        /// Enable response status code logging
        /// </summary>
        /// <returns>The current options builder</returns>
        public LoggerOptionsBuilder AddStatusCode()
        {
            _loggerOptions.ShowStatusCode = true;
            return this;
        }

        /// <summary>
        /// Enable response length logging
        /// </summary>
        /// <returns>The current options builder</returns>
        public LoggerOptionsBuilder AddResponseLength()
        {
            _loggerOptions.ShowResponseLength = true;
            return this;
        }

        /// <summary>
        /// Enable response time logging
        /// </summary>
        /// <returns>The current options builder</returns>
        public LoggerOptionsBuilder AddResponseTime()
        {
            _loggerOptions.ShowResponseTime = true;
            return this;
        }

        /// <summary>
        /// Generate the logger options from the builder
        /// </summary>
        /// <returns>The logger options</returns>
        public LoggerOptions Build()
        {
            return _loggerOptions;
        }
    }
}