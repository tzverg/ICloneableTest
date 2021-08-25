#region preprocessor directives
    // #define TEST_A
    // #define TEST_D

    // #define TEST_DIRECT_ASSIGNMENT
    // #define TEST_ICLONABLE
    // #define TEST_REFLECTION_DEEP_COPY

    // #define CREATE_INSTANCE
    // #define COPY_INSTANCE
    // #define CHANGE_DATA
    // #define PRINT_INFO
    // #define PRINT_MESSAGES
    // #define TEST_APF
    // #define TEST_DIRECTIVES
    #define TEST_LAMBDA
#endregion

using System;
using AR_496;

#if TEST_LAMBDA
    using AR_497;
#endif

enum OperationType
{
    ADD, SUBSTRACT, MULTIPLY, DIVIDE
}

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
            #if TEST_DIRECTIVES
                TestPreprocessorDirectives();
            #endif

            #if TEST_APF
                TestEvent();
                TestDelegateAPF(false, false, true);
            #endif

            #if TEST_LAMBDA
                Calculation.TestLambdaFunction();
            #endif
        }

        private static void TestDelegateAPF(bool testAction, bool testPredicate, bool testFunc)
        {
            if (testAction)
            {
                Action<int, int, OperationType> mathOperation = MathOperationEvent;
                MathOperation(4, 2, mathOperation);
            }

            if (testPredicate)
            {
                Predicate<int> isPositive = delegate (int x) { return x > 0; };

                Console.WriteLine(isPositive(1));
                Console.WriteLine(isPositive(-1));
                Console.WriteLine(isPositive(0));
            }

            if (testFunc)
            {
                Func<int, int> retFunc = GetFactorial;

                int resultA = GetResult(4, retFunc);
                int resultB = GetResult(4, x => (x - 1));

                Console.WriteLine($"Test Func delegate, factorial: {resultA}");
                Console.WriteLine($"Test Func delegate: {resultB}");
            }
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

        private static int GetResult(int mainValue, Func<int, int> returnFunc)
        {
            int result = 0;

            if (mainValue > 0)
            {
                result = returnFunc(mainValue);
            }

            return result;
        }

        private static int GetFactorial(int x)
        {
            int resultValue = 1;
            for (int cnt = 1; cnt <= x; cnt++)
            {
                resultValue *= cnt;
            }
            return resultValue;
        }

        private static void MathOperation(int operandX, int operadnY, Action<int, int, OperationType> mathOperation)
        {
            if ((operandX * operadnY) > 0)
            {
                mathOperation(operandX, operadnY, OperationType.MULTIPLY);
            }
        }

        private static void MathOperationEvent(int operandX, int operadnY, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.MULTIPLY:
                    Console.WriteLine($"Произведение чисел: " + (operandX * operadnY));
                    break;
                case OperationType.DIVIDE:
                    Console.WriteLine($"Частное чисел: " + (operandX / operadnY));
                    break;
                case OperationType.ADD:
                    Console.WriteLine($"Сумма чисел: " + (operandX + operadnY));
                    break;
                case OperationType.SUBSTRACT:
                    Console.WriteLine($"Разность чисел: " + (operandX - operadnY));
                    break;
                default:
                    Console.WriteLine($"Нет определения для указанной операции");
                    break;
            }
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