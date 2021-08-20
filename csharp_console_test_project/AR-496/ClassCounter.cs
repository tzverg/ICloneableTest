using System;
using Testnamespace;

public class ClassCounter 
{
    public event EventMessage onCount;
    public void Count()
    { 
        for (int cnt = 0; cnt < 10; cnt++)
        {
            Console.WriteLine($"cnt {cnt}");

            if (cnt == 7)
            {
                onCount?.Invoke("шевелись, засранец, уже 7");
            }
        }
    }
}