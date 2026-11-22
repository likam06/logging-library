public class Display
{
    //stores display information
    private string model;
    private int width;
    private int height;
    private float ppi;

    //initializes a display
    public Display(string model, int width, int height, float ppi)
    {
        this.model = model;
        this.width = width;
        this.height = height;
        this.ppi = ppi;
    }
        //compares the physical size of two displays
    public string CompareSize(Display other)
    {
        double size1 = Math.Sqrt(width * (double)width + height * (double)height) / ppi;
        double size2 = Math.Sqrt(other.width * (double)other.width + other.height * (double)other.height) / other.ppi;

    if (size1 > size2)
    {
        return $"{model} ({size1:F1}\") is bigger than {other.model} ({size2:F1}\")";
    }
    else if (size1 < size2)
    {
        return $"{model} ({size1:F1}\") is smaller than {other.model} ({size2:F1}\")";
    }
    else
    {
        return $"{model} and {other.model} have the same screen size";
    }
}
//compares the sharpness of two displays
public string CompareSharpness(Display other)
{
    if (ppi > other.ppi)
    {
        return $"{model} is sharper than {other.model}";
    }
    else if (ppi < other.ppi)
    {
        return $"{model} is less sharp than {other.model}";
    }
    else
    {
        return $"{model} and {other.model} have the same sharpness";
    }
}
//compares two displays and gives a final verdict
public string CompareWithMonitor(Display other)
{
    string sizeResult = CompareSize(other);
    string sharpnessResult = CompareSharpness(other);

    double size1 = Math.Sqrt(width * (double)width + height * (double)height) / ppi;
    double size2 = Math.Sqrt(other.width * (double)other.width + other.height * (double)other.height) / other.ppi;

    string verdict;

    if (size1 > size2 && ppi > other.ppi)
    {
        verdict = model + " is both bigger and sharper.";
    }
    else if (size1 < size2 && ppi < other.ppi)
    {
        verdict = other.model + " is both bigger and sharper.";
    }
    else
    {
        verdict = "Neither display is better in both size and sharpness.";
    }

    return sizeResult + "\n" + sharpnessResult + "\nFinal verdict: " + verdict;
}
    }
