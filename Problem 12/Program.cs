using ProjectEuler.Helpers;
using System;
using System.Collections.Generic;

namespace Problem_12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 12");
            int i = 0;
            int currentSum = 0;
            int minDivisors = 10;
            NumberCalculations calc = new();
            HashSet<int> test;
            do
            {
                i++;
                currentSum += i;
                test = calc.FactorsFast(currentSum);
                Console.WriteLine($"Current trianglenumber: {currentSum}; Current divisors: {String.Join(";", test)}");

            } while (test.Count <= minDivisors);

            Console.WriteLine($"trianglenumber {currentSum}: {string.Join(";", calc.FactorsFast(currentSum))}");

        }
    }
}
