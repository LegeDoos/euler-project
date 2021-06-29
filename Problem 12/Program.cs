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
            int minDivisors = 500;
            int currentMaxDivisors = 0;
            HashSet<int> result;
            do
            {
                i++;
                currentSum += i;
                result = NumberCalculations.GetAllFactors(currentSum);
                if (result.Count > currentMaxDivisors)
                {
                    currentMaxDivisors = result.Count;
                    Console.WriteLine($"Current trianglenumber with max divisors: {i} with sum {currentSum}; Current divisors: {currentMaxDivisors}");
                }
            } while (result.Count <= minDivisors);

            Console.WriteLine($"Result: trianglenumber {i} with value {currentSum}: {string.Join(";", result)}");
           

        }
    }
}
