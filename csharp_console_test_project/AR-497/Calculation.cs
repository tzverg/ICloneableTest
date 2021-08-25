using System;

namespace AR_497
{
    public enum OperationType
    {
        ADD, SUBTRACT, MULTIPLY, DIVIDE
    }

    public delegate int Operation(int x, int y);
    public delegate void Print(OperationType operationType, int valueX, int valueY);
    public delegate bool IsEqual(int x);

    public static class Calculation
    {            
        private static Operation operationSumm = (x, y) => x + y;
        private static Operation operationSubt = (x, y) => x - y;
        private static Operation operationMult = (x, y) => x * y;
        private static Operation operationDiv = (x, y) => x / y;

        public static void TestLambdaFunction()
        {
            printResult(AR_497.OperationType.ADD, 3, 4);
            printResult(AR_497.OperationType.SUBTRACT, 2, 5);
            printResult(AR_497.OperationType.MULTIPLY, 6, 3);
            printResult(AR_497.OperationType.DIVIDE, 8, 3);

            int[] integers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int result1 = GetResult(integers, AR_497.OperationType.ADD, x => x > 5);
            int result2 = GetResult(integers, AR_497.OperationType.ADD, x => x % 2 == 0);

            Console.WriteLine($"result1: {result1}");
            Console.WriteLine($"result2: {result2}");
        }

        private static int GetResult(int[] numbers, OperationType operationType, IsEqual func)
        {
            int result = 0;

            foreach (int itemTarget in numbers)
            {
                if (func(itemTarget))
                {
                    switch (operationType)
                    {
                        case OperationType.ADD:
                            result += itemTarget;
                            break;
                        case OperationType.MULTIPLY:
                            result *= itemTarget;
                            break;
                        default:
                            Console.WriteLine($"operation {OperationToFriendlyString(operationType)} do not supported");
                            break;
                    }
                }
            }

            return result;
        }

        private static Print printResult = (OperationType operationType, int valueX, int valueY) =>
        {
            switch (operationType)
            {
                case OperationType.ADD:
                    Console.WriteLine($"{valueX} {OperationToFriendlyString(operationType)} {valueY} = {operationSumm(valueX, valueY)}");
                    break;
                case OperationType.SUBTRACT:
                    Console.WriteLine($"{valueX} {OperationToFriendlyString(operationType)} {valueY} = {operationSubt(valueX, valueY)}");
                    break;
                case OperationType.MULTIPLY:
                    Console.WriteLine($"{valueX} {OperationToFriendlyString(operationType)} {valueY} = {operationMult(valueX, valueY)}");
                    break;
                case OperationType.DIVIDE:
                    Console.WriteLine($"{valueX} {OperationToFriendlyString(operationType)} {valueY} = {operationDiv(valueX, valueY)}");
                    break;
                default:
                    Console.WriteLine($"operation {OperationToFriendlyString(operationType)} do not supported");
                    break;
            }
        };

        private static string OperationToFriendlyString(OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.ADD:
                    return "+";
                case OperationType.SUBTRACT:
                    return "-";
                case OperationType.MULTIPLY:
                    return "*";
                case OperationType.DIVIDE:
                    return "/";
                default:
                    return "invalid";
            }
        }
    }
}