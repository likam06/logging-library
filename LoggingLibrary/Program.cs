
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