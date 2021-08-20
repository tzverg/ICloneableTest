﻿#region preprocessor directives
    #define TEST_A
    // #define TEST_D

    // #define TEST_DIRECT_ASSIGNMENT
    #define TEST_ICLONABLE
    // #define TEST_REFLECTION_DEEP_COPY

    #define CREATE_INSTANCE
    #define COPY_INSTANCE
    #define CHANGE_DATA
    // #define PRINT_INFO
    #define PRINT_MESSAGES
#endregion

using System;
using AR_496;

namespace Testnamespace
{
    #region namespace delegates
        public delegate string DelegateMessage();
        public delegate void EventMessage(string value);
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            //TestPreprocessorDirectives();

            TestEvent();
        }

        private static void TestEvent()
        {
            ClassCounter classCounter = new ClassCounter();
            classCounter.onCount += new MessageHandler().Message;
            classCounter.Count();
        }

        private static void TestPreprocessorDirectives()
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

            #region print messages
                #if PRINT_MESSAGES
                    #if TEST_A
                                classA1.classB.messageB += classA1.classB.messageB;
                                classA1.classB.messageB += classA1.classB.messageB;
                                classA1.classB.messageB?.Invoke();
                                classA1.classB.messageB -= classA1.classB.messageB;
                                classA1.classB.messageB?.Invoke();
                                classA1.classB.messageB -= classA1.classB.messageB;
                                classA1.classB.messageB?.Invoke();
                    #elif TEST_D
                                            classD1.exampleClassC.messageC?.Invoke();
                                            classD2.exampleClassC.messageC?.Invoke();
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
                T classCopy = ValidateAndCopy(classTarget);
            #else
                Console.WriteLine($"copy class {classTarget.ToString()} by Clone method that use reflection");
                T classCopy = (T)ReflectionDeepCopy.Clone(classTarget);
            #endif

            return classCopy;
        }

        private static T ValidateAndCopy<T>(T classTarget)
        {
            Type t = typeof(T);
            object[] attrs = t.GetCustomAttributes(false);
            T classCopy = default(T);

            foreach (CloneableValidationAttribute attr in attrs)
            {
                if (attr.CloneableValidate<T>(classTarget))
                {
                    classCopy = (T)(classTarget as ICloneable).Clone();
                }
            }

            if (classCopy == null)
            {
                Console.WriteLine($"class {classTarget.ToString()} does not implement the IClonable interface, than copy class by Clone method that use reflection");
                classCopy = (T)ReflectionDeepCopy.Clone(classTarget);
            }

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