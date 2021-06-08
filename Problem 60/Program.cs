using Facet.Combinatorics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Problem_60
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start");
            PrimeNumbers primes = new();
            primes.CalculateTo = 1000000;
            primes.CalculatePrimeNumbers();
            Problem60Solver solver = new(primes, false, false);
            solver.NumberOfNodes = 4;
            solver.MaxPrime = 1000;
            var result = solver.Solve();
            Console.WriteLine($"{result}");
        }
    }
}
