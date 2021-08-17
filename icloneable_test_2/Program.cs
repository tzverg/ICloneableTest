using System;

namespace iclonable_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleClassA classA1 = new ExampleClassA() { PropertyName="A1", PropertyAge="1" };
            ExampleClassA classA2 = (ExampleClassA)classA1.Clone();
            //ExampleClassA classA2 = (ExampleClassA)ReflectionDeepCopy.Clone(classA1);

            PrintClassData(classA1);
            PrintClassData(classA2);

            classA2.PropertyName = "A2";
            classA2.classB.stringValue = "changed class B";

            PrintClassData(classA1);
            PrintClassData(classA2);
        }

        private static void PrintClassData(ExampleClassA targetClass)
        {
            Console.WriteLine($"{targetClass.PropertyAge} {targetClass.PropertyName} {targetClass.classB.stringValue}");
        }
    }
}