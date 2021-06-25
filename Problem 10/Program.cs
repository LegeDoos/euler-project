using ProjectEuler.Helpers;
using System;
using System.Linq;

namespace Problem_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 10");
            PrimeNumbers prim = new();
            prim.CalculateTo = 2000000;
            prim.CalculatePrimeNumbers(true);
            Console.WriteLine($"Result of all primes below 2000000 = {prim.Numbers.Sum()}");
        }
    }
}
