namespace Logly.Models;

internal class Response
{
    public int StatusCode { get; set; }

    public long ResponseLength { get; set; }

    public double ResponseTime { get; set; }

    public ConsoleColor StatusCodeColor =>
        StatusCode switch
        {
            < 200 => ConsoleColor.White,
            >= 200 and < 300 => ConsoleColor.Green,
            >= 300 and < 400 => ConsoleColor.DarkBlue,
            >= 400 and < 500 => ConsoleColor.Yellow,
            _ => ConsoleColor.Red
        };
}
