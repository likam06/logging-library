using System;
using System.Collections.Generic;

public class DisplayManager
{
    //stores the manager name
    private string name;

    //stores assigned displays
    private List<Display> assignedDisplays = new List<Display>();

    //initializes a manager
    public DisplayManager(string name)
    {
        this.name = name;
    }

    //assigns a display to the manager
    public void AssignDisplay(Display display)
    {
        assignedDisplays.Add(display);
    }

    //compares assigned displays
    public void Assist()
    {
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
            return display;
        }

        return null;
    }
}