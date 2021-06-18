using System;

namespace Problem_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 5");
            /*
            2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
            What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20 ?
            */
            int max = 20;
            int currentNumber = max;
            bool found = true;
            do
            {
                found = true;
                currentNumber++;
                int currentDivide = 1;
                while (found && currentDivide <= max)
                {
                    found = currentNumber % currentDivide == 0;
                    currentDivide++;
                }
            } while (!found);
            Console.WriteLine($"Result: {currentNumber}");
        }
    }
}
