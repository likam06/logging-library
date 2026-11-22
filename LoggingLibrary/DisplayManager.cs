
using System;
using System.Collections.Generic;

public class DisplayManager
{
    //stores the manager name
    private string name;

    //stores assigned displays
    private List<Display> assignedDisplays = new List<Display>();

    //stores the manager logger
    private Logger logger;

    //returns the manager name
    public string Name
    {
        get { return name; }
    }

    //initializes a manager
    public DisplayManager(string name)
    {
        this.name = name;
        this.logger = new Logger("Manager");
        logger.Log(LogLevel.TRACE, "Manager " + name + " created");
    }

    //initializes a manager with an existing logger
    public DisplayManager(string name, Logger logger)
    {
        this.name = name;
        this.logger = logger;
        logger.Log(LogLevel.TRACE, "Manager " + name + " created");
    }

    //assigns a display to the manager
    public void AssignDisplay(Display display)
    {
        if (assignedDisplays.Contains(display))
        {
            logger.Log(LogLevel.WARN, "Display already assigned to " + name);
            return;
        }

        assignedDisplays.Add(display);
        logger.Log(LogLevel.TRACE, "Display assigned to " + name);
    }

    //compares assigned displays
    public void Assist()
    {
        if (assignedDisplays.Count < 2)
        {
            logger.Log(LogLevel.WARN, name + " does not have enough displays to compare");
            return;
        }

        for (int i = 0; i < assignedDisplays.Count - 1; i++)
        {
            Console.WriteLine(assignedDisplays[i].CompareWithMonitor(assignedDisplays[i + 1]));
        }
    }

    //removes a purchased display
    public Display? BuyDisplay(Display display)
    {
        if (assignedDisplays.Remove(display))
        {
            logger.Log(LogLevel.INFO, "Display sold by " + name);

            if (assignedDisplays.Count == 0)
            {
                logger.Log(LogLevel.WARN, name + " has no displays left");
            }

            return display;
        }

        logger.Log(LogLevel.ERROR, "Display is not assigned to " + name);
        return null;
    }
}
