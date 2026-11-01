
using System;

public class ParenthesesValidator
{
    //creates a logger for the validator
   private Logger logger = new Logger("Parentheses");

    //checks if parentheses are balanced and correctly ordered
    public bool IsValid(string input)
    {
        int balance = 0;

        foreach (char symbol in input)
        {
            if (symbol == '(')
            {
                balance++;
                logger.Log(LogLevel.TRACE, "Opening parenthesis found");
            }
            else if (symbol == ')')
            {
                balance--;

                if (balance < 0)
                {
                    logger.Log(LogLevel.ERROR, "Invalid input: closing parenthesis without a matching opening one. Input: " + input);
                    return false;
                }

                logger.Log(LogLevel.INFO, "Matching pair found");
            }
        }

        bool result = balance == 0;

        if (result)
        {
            logger.Log(LogLevel.INFO, "Validation result: valid");
        }
        else
        {
            logger.Log(LogLevel.ERROR, "Invalid input: unclosed opening parenthesis. Input: " + input);
        }

        return result;
    }
}
