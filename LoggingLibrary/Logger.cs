
using System;

public class Logger
{
    //stores the logger name
    private string name;
    //stores the formatter used by this logger
    private Formatter formatter;

    //stores the sink used by this logger
    private ConsoleSink sink;

    //shared settings for all logger objects
    private static LogLevel level = LogLevel.TRACE;



    //initializes a logger with an existing formatter and sink
    public Logger(string name, Formatter formatter, ConsoleSink sink)
    {
        this.name = name;
        this.formatter = formatter;
        this.sink = sink;
    }
    //initializes a logger with its own formatter and sink
    public Logger(string name)
    {
        this.name = name;
        this.formatter = new Formatter();
        this.sink = new ConsoleSink();
    }

    //changes the minimum level of messages to display
    public static void SetLevel(LogLevel newLevel)
    {
        level = newLevel;
    }

    

//writes a message if its level is allowed
    public void Log(LogLevel messageLevel, string message)
{
    if (messageLevel < level)
    {
        return;
    }

    string output = formatter.Format(messageLevel, name, message);

    sink.Write(output);
}

    }

