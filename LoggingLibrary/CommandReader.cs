using System;
using System.IO;

public class CommandReader
{
    //stores the logger
    private Logger logger;

    public CommandReader(Logger logger)
    {
        this.logger = logger;
    }

    //reads commands from a file
    public string[] ReadCommands(string path)
    {
        if (!File.Exists(path))
        {
            logger.Log(LogLevel.ERROR, "File not found: " + path);
            return Array.Empty<string>();
        }

        return File.ReadAllLines(path);
    }
}