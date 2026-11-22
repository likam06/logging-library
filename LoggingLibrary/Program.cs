
using System;
using System.Collections.Generic;
using System.Globalization;

//creates a validator object
ParenthesesValidator validator = new ParenthesesValidator();

//sets the minimum logging level
Logger.SetLevel(LogLevel.TRACE);

//tests different parentheses combinations
string[] testCases =
{
    "()",
    ")((()))",
    "(",
    "(())((()())())"
};

foreach (string input in testCases)
{
    Console.WriteLine("\nTesting: " + input);

    bool result = validator.IsValid(input);

    Console.WriteLine("Result: " + result);
}

//tests random messages
Formatter.SetFormat("[%t] [%l] [%n]: %r");

Console.WriteLine("\nTesting random messages:");
validator.IsValid("()");
//tests log level filtering
Console.WriteLine("\nTesting INFO level:");

Logger.SetLevel(LogLevel.INFO);
Formatter.SetFormat("[%l] [%n]: %m");

validator.IsValid("()");
//tests custom log format
Console.WriteLine("\nTesting custom format:");

Logger.SetLevel(LogLevel.TRACE);
Formatter.SetFormat("%l - %m");

validator.IsValid("()");

//tests WARN and ERROR visibility
Console.WriteLine("\nTesting WARN and ERROR:");

Logger.SetLevel(LogLevel.INFO);

Logger testLogger = new Logger("TestLogger");

testLogger.Log(LogLevel.WARN, "Warning message");
testLogger.Log(LogLevel.ERROR, "Error message");
//tests the formatter separately
Console.WriteLine("\nTesting Formatter:");

Formatter formatter = new Formatter();

string formattedMessage = formatter.Format(
    LogLevel.INFO,
    "TestLogger",
    "Hello from Formatter"
);

Console.WriteLine(formattedMessage);
//tests a custom formatter format
Formatter.SetFormat("[%l] %n: %m");

Console.WriteLine(formatter.Format(
    LogLevel.WARN,
    "TestLogger",
    "Custom format test"
));
//tests aggregation with shared objects
Console.WriteLine("\nTesting Aggregation:");

Formatter sharedFormatter = new Formatter();
ConsoleSink sharedSink = new ConsoleSink();

Logger firstLogger = new Logger("First", sharedFormatter, sharedSink);
Logger secondLogger = new Logger("Second", sharedFormatter, sharedSink);

firstLogger.Log(LogLevel.INFO, "First logger message");
secondLogger.Log(LogLevel.INFO, "Second logger message");
//tests display comparison
Console.WriteLine("\nTesting Display:");

Display dell = new Display("Dell-U2723QE", 3840, 2160, 163.0f);
Display lg = new Display("LG-27GP850", 2560, 1440, 108.8f);
Display samsung = new Display("Samsung-Odyssey-G5", 2560, 1440, 93.0f);

Console.WriteLine(dell.CompareWithMonitor(lg));
Console.WriteLine(lg.CompareWithMonitor(samsung));
Console.WriteLine(samsung.CompareWithMonitor(dell));
//tests display managers
Console.WriteLine("\nTesting DisplayManager:");

DisplayManager bob = new DisplayManager("Bob");
DisplayManager alice = new DisplayManager("Alice");

//assigns displays to managers
bob.AssignDisplay(dell);
bob.AssignDisplay(lg);
bob.AssignDisplay(samsung);

alice.AssignDisplay(dell);
alice.AssignDisplay(samsung);

//tests manager assistance
Console.WriteLine("\nBob compares displays:");
bob.Assist();

//tests buying a display
Console.WriteLine("\nBob buys Dell:");
bob.BuyDisplay(dell);

//tests the remaining displays
Console.WriteLine("\nBob after purchase:");
bob.Assist();

Console.WriteLine("\nAlice after Bob's purchase:");
alice.Assist();
//tests reading commands from a file
Console.WriteLine("\nTesting CommandReader:");

Logger readerLogger = new Logger("CommandReader");
CommandReader reader = new CommandReader(readerLogger);

string[] commands = reader.ReadCommands("LoggingLibrary/shop-monday.txt");
//stores managers and displays created from commands
List<DisplayManager> shopManagers = new List<DisplayManager>();
List<Display> shopDisplays = new List<Display>();


Logger.SetLevel(LogLevel.TRACE);
Formatter.SetFormat("[%l] %n: %m");

Logger parserLogger = new Logger("Parser", sharedFormatter, sharedSink);

//finds a manager by name
DisplayManager? FindManager(string name)
{
    foreach (DisplayManager manager in shopManagers)
    {
        if (manager.Name == name)
        {
            return manager;
        }
    }

    return null;
}

//finds a display by model
Display? FindDisplay(string model)
{
    foreach (Display display in shopDisplays)
    {
        if (display.Model == model)
        {
            return display;
        }
    }

    return null;
}

//processes commands from the file
for (int i = 0; i < commands.Length; i++)
{
    string[] words = commands[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    int line = i + 1;

    //skips empty lines and comments
    if (words.Length == 0 || words[0].StartsWith("#"))
    {
        continue;
    }

    //creates a manager
    if (words[0] == "MANAGER")
    {
        if (words.Length != 2)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Invalid MANAGER command");
            continue;
        }

        if (FindManager(words[1]) != null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Manager already exists");
            continue;
        }

        DisplayManager manager = new DisplayManager(
            words[1],
            new Logger("Manager", sharedFormatter, sharedSink)
        );

        shopManagers.Add(manager);
    }

    //creates a display
    else if (words[0] == "DISPLAY")
    {
        if (words.Length != 5 ||
            !int.TryParse(words[2], out int width) ||
            !int.TryParse(words[3], out int height) ||
            !float.TryParse(words[4], NumberStyles.Float, CultureInfo.InvariantCulture, out float ppi) ||
            width <= 0 || height <= 0 || ppi <= 0)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Invalid DISPLAY command");
            continue;
        }

        if (FindDisplay(words[1]) != null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Display already exists");
            continue;
        }

        Display display = new Display(
            words[1], width, height, ppi,
            new Logger("Display", sharedFormatter, sharedSink)
        );

        shopDisplays.Add(display);
    }

    //assigns a display to a manager
    else if (words[0] == "ASSIGN")
    {
        if (words.Length != 3)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Invalid ASSIGN command");
            continue;
        }

        Display? display = FindDisplay(words[1]);
        DisplayManager? manager = FindManager(words[2]);

        if (display == null || manager == null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Display or manager not found");
            continue;
        }

        manager.AssignDisplay(display);
    }

    //compares two displays
    else if (words[0] == "COMPARE")
    {
        if (words.Length != 3)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Invalid COMPARE command");
            continue;
        }

        Display? first = FindDisplay(words[1]);
        Display? second = FindDisplay(words[2]);

        if (first == null || second == null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Display not found");
            continue;
        }

        Console.WriteLine(first.CompareWithMonitor(second));
    }

    //helps a manager compare assigned displays
    else if (words[0] == "ASSIST")
    {
        if (words.Length != 2)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Invalid ASSIST command");
            continue;
        }

        DisplayManager? manager = FindManager(words[1]);

        if (manager == null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Manager not found");
            continue;
        }

        Console.WriteLine("\n" + manager.Name + " compares displays:");
        manager.Assist();
    }

    //removes a purchased display from a manager
    else if (words[0] == "BUY")
    {
        if (words.Length != 3)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Invalid BUY command");
            continue;
        }

        Display? display = FindDisplay(words[1]);
        DisplayManager? manager = FindManager(words[2]);

        if (display == null || manager == null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Display or manager not found");
            continue;
        }

        if (manager.BuyDisplay(display) == null)
        {
            parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Purchase failed");
        }
    }

    //handles unknown commands
    else
    {
        parserLogger.Log(LogLevel.ERROR, "Line " + line + ": Unknown command " + words[0]);
    }
}
