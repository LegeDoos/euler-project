using System;

namespace Problem_14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 14");
            int start = Environment.TickCount;
            long startingNumber = 1000000;
            Collatz collatz = new();

            int currentLength = 0;
            long currentStartingNumber = 0;
            for (long i = startingNumber; i > 1; i--)
            {
                Console.WriteLine($"Calculate sequence for {i}");
                if (collatz.GetLength(i) > currentLength)
                {
                    currentLength = collatz.GetLength(i);
                    currentStartingNumber = i;
                }
            }

            Console.WriteLine($"Result: Starting number {currentStartingNumber} with terms {string.Join("->", collatz.GetTerms(currentStartingNumber))} in {Environment.TickCount - start} ms");
            collatz.SaveToFileForFun();
        }
    }
}
