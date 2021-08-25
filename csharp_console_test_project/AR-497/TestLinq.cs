using System;
using System.Linq;

namespace AR_497
{
    public static class TestLinq
    {
        public static void TestLinqToObjects()
        {
            string[] sourceString = new string[]
                { "az", "buki", "vedi", "glagol", "dobro", "yest", "jivete", "zelo", "zemlya", "izhe" };

            int[] sourceIntArray = new int[]
            { 1, 2, 3, 4 };

            string[] selectedTeams = LinqSelectStringsByFirstLetter(sourceString, 'z');

            int resultAggregateSumm = LinqAggregateCollectionWithTemplate(sourceIntArray, (x, y) => x + y);
            int resultAggregateMult = LinqAggregateCollectionWithTemplate(sourceIntArray, (x, y) => x * y);

            foreach (string targetString in sourceString)
                Console.WriteLine(targetString);

            Console.WriteLine($"\nresult of selecting string by first letter 'z':\n");

            foreach (string targetString in selectedTeams)
                Console.WriteLine(targetString);

            Console.WriteLine($"\n-----------------------------------------------\n");

            foreach (int targetInt in sourceIntArray)
                Console.WriteLine(targetInt);

            Console.WriteLine($"\nresult aggregate by template summ: {resultAggregateSumm}");
            Console.WriteLine($"result aggregate by template mult: {resultAggregateMult}\n");
        }
        private static string[] LinqSelectStringsByFirstLetter(string[] stringSourceArray, char firstLetter)
        {
            var resultStringList = from targetString in stringSourceArray
                                    where targetString.ToLower().StartsWith(firstLetter)
                                    orderby targetString ascending
                                    select targetString;
            return resultStringList.ToArray<string>();
        }

        private static int LinqAggregateCollectionWithTemplate(int[] targetNumbers, System.Func<int, int, int> templateFunc)
        {
            int resultNumber = targetNumbers.Aggregate(templateFunc);
            return resultNumber;
        }
    }
}