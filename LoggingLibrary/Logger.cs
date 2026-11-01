
using System;

public class Logger
{
    //stores the logger name
    private string name;

    //shared settings for all logger objects
    private static LogLevel level = LogLevel.TRACE;
    private static string format = "[%t] [%l] [%n]: %m";

    //generates random messages
    private static Random random = new Random();

    //initializes a logger with its name
    public Logger(string name)
    {
        this.name = name;
    }

    //changes the minimum level of messages to display
    public static void SetLevel(LogLevel newLevel)
    {
        level = newLevel;
    }

    //changes the output format
    public static void SetFormat(string newFormat)
    {
        format = newFormat;
    }

    //writes a message if its level is allowed
    public void Log(LogLevel messageLevel, string message)
    {
        if (messageLevel < level)
        {
            return;
        }

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

        //replaces format placeholders with actual values
        output = output.Replace("%t", DateTime.Now.ToString("HH:mm:ss"));
        output = output.Replace("%m", message);
        output = output.Replace("%n", name);
        output = output.Replace("%l", messageLevel.ToString());
        output = output.Replace("%r", randomMessages[random.Next(randomMessages.Length)]);

        Console.WriteLine(output);
    }
}
