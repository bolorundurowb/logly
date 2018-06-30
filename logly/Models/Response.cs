using System;

namespace logly.Models
{
    internal class Response
    {
        public int StatusCode { get; set; }
        public long ResponseLength { get; set; }
        public double ResponseTime { get; set; }

        public ConsoleColor StatusCodeColor
        {
            get
            {
                if (StatusCode < 200)
                {
                    return ConsoleColor.White;
                }

                if (StatusCode >= 200 && StatusCode < 300)
                {
                    return ConsoleColor.Green;
                }

                if (StatusCode >= 300 && StatusCode < 400)
                {
                    return ConsoleColor.DarkBlue;
                }

                if (StatusCode >= 400 && StatusCode < 500)
                {
                    return ConsoleColor.Yellow;
                }

                return ConsoleColor.Red;
            }
        }
    }
}