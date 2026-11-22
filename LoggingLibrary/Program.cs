
using System;

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

for (int i = 0; i < commands.Length; i++)
{
    Console.WriteLine((i + 1) + ": " + commands[i]);
}