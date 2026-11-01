
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
Logger.SetFormat("[%t] [%l] [%n]: %r");

Console.WriteLine("\nTesting random messages:");
validator.IsValid("()");
//tests log level filtering
Console.WriteLine("\nTesting INFO level:");

Logger.SetLevel(LogLevel.INFO);
Logger.SetFormat("[%l] [%n]: %m");

validator.IsValid("()");
//tests custom log format
Console.WriteLine("\nTesting custom format:");

Logger.SetLevel(LogLevel.TRACE);
Logger.SetFormat("%l - %m");

validator.IsValid("()");

//tests WARN and ERROR visibility
Console.WriteLine("\nTesting WARN and ERROR:");

Logger.SetLevel(LogLevel.INFO);

Logger testLogger = new Logger("TestLogger");

testLogger.Log(LogLevel.WARN, "Warning message");
testLogger.Log(LogLevel.ERROR, "Error message");
