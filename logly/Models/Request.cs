namespace logly.Models
{
    internal class Request
    {
        public RequestType Type { get; set; }
        public string Url { get; set; }
    }
    
    internal enum RequestType
    {
        GET,
        POST,
        PUT,
        DELETE,
        PATCH
    }
}