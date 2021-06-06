using System;
using System.IO;

namespace Problem_60
{
    class Program
    {
        static void Main()
        {
            PrimeNumbers primes = new();
            primes.CalculateTo = 673109;
            primes.CalculatePrimeNumbers();
            Problem60Solver solver = new(primes);
            solver.NumberOfNodes = 3;
            var result = solver.Solve();
            Console.WriteLine($"{result}");
        }
    }
}
