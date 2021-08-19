#region preprocessor directives
    #define TEST_A
    // #define TEST_D

    // #define TEST_DIRECT_ASSIGNMENT
    #define TEST_ICLONABLE
    // #define TEST_REFLECTION_DEEP_COPY

    #define CREATE_INSTANCE
    #define COPY_INSTANCE
    #define CHANGE_DATA
    #define PRINT_INFO
#endregion

using System;

namespace iclonable_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region create class instance
                #if CREATE_INSTANCE
                    #if TEST_A
                        ExampleClassA classA1 = new ExampleClassA { PropertyName = "A1", PropertyAge = "1" };
                    #elif TEST_D 
                        ExampleClassD classD1 = new ExampleClassD { exampleClassC = new ExampleClassC() };
                    #endif
                #endif
            #endregion

            #region copy class instance
                #if CREATE_INSTANCE && COPY_INSTANCE
                    #if TEST_A
                        ExampleClassA classA2 = CopyClass(classA1);
                    #elif TEST_D
                        ExampleClassD classD2 = CopyClass(classD1);
                    #endif
                #endif
            #endregion

            #region change class data
                #if CREATE_INSTANCE && COPY_INSTANCE && CHANGE_DATA
                    #if TEST_A
                        classA2.PropertyName = "A2";
                        classA2.PropertyAge = "2";
                        classA2.classB.stringValue = "changed value B";
                    #elif TEST_D
                        classD2.exampleClassC.cStringValue = "changed value C";
                    #endif
                #endif
            #endregion

            #region print copy info
                #if CREATE_INSTANCE && COPY_INSTANCE && PRINT_INFO
                    #if TEST_A
                        PrintClassData(classA1);
                        PrintClassData(classA2);
                    #elif TEST_D 
                        PrintClassData(classD1);
                        PrintClassData(classD2);
                    #endif
                #endif
            #endregion
        }

        private static T CopyClass<T>(T classTarget)
        {
            #if TEST_DIRECT_ASSIGNMENT
                Console.WriteLine($"copy class {classTarget.ToString()} by value");
                T classCopy = classTarget;
            #elif TEST_ICLONABLE
                Console.WriteLine($"copy class {classTarget.ToString()} by Clone method of ICloneable interface");
                T classCopy = (T)(classTarget as ICloneable).Clone();
            #else
                Console.WriteLine($"copy class {classTarget.ToString()} by Clone method that use reflection");
            T classCopy = (T)ReflectionDeepCopy.Clone(classTarget);
            #endif

            return classCopy;
        }

        private static void PrintClassData<T>(T targetClass)
        {
            #if TEST_A
                Console.WriteLine
                (
                    targetClass.ToString() + 
                    " { " + 
                        $"{(targetClass as ExampleClassA).PropertyAge}, " +
                        $"{(targetClass as ExampleClassA).PropertyName}, " +
                        $"{(targetClass as ExampleClassA).classB.stringValue} " +
                    " } "
                );
            #elif TEST_D
                Console.WriteLine
                (
                    targetClass.ToString() + 
                    " { " + 
                        $"{(targetClass as ExampleClassD).dStringValue}, " +
                        $"{(targetClass as ExampleClassD).exampleClassC.cStringValue}" +
                    " } "
                );
            #endif
        }
    }
}