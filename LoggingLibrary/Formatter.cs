using System;

public class Formatter
{
    //stores the format for log messages
    private static string format = "[%t] [%l] [%n]: %m";
    //generates random messages
    private static Random random = new Random();

    //changes the output format
    public static void SetFormat(string newFormat)
    {
        format = newFormat;
    }
    //formats a log message
    public string Format(LogLevel messageLevel, string name, string message)
    {
        string output = format;
        //selects random messages based on the log level
    string[] randomMessages = messageLevel switch
    {
        LogLevel.TRACE => new[] { "Detailed execution step", "Checking internal process", "Debug information" },
        LogLevel.INFO => new[] { "Operation completed", "Process running", "Information update" },
        LogLevel.WARN => new[] { "Attention required", "Potential issue detected", "Check this situation" },
        LogLevel.ERROR => new[] { "Critical problem occurred", "Operation failed", "An error was detected" },
        _ => new[] { "Log message" }
    };
        output = output.Replace("%t", DateTime.Now.ToString("HH:mm:ss"));
        output = output.Replace("%m", message);
        output = output.Replace("%n", name);
        output = output.Replace("%l", messageLevel.ToString());
        output = output.Replace("%r", randomMessages[random.Next(randomMessages.Length)]);
        return output;
    }
}