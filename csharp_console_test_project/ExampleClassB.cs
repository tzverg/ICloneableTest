using System;

class ExampleClassB
{
    public string stringValue = "default value B";
    protected bool boolValue = true;
    internal int intValue = 4;
    private float floatValue = 6.3F;
    protected internal double doubleValue = 2.2;

    protected private byte PropertyValue { get; private set; }

    public ExampleClassB()
    {
        // Console.WriteLine($"created " + this.ToString());
    }
}