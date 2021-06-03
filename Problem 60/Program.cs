using System;
using System.IO;

namespace Problem_60
{
    class Program
    {
        static void Main()
        {
            PrimeNumbers primes = new();
            primes.CalculateTo = 110000;
            primes.CalculatePrimeNumbers();
            Problem60Solver solver = new(primes);
            solver.NumberOfNodes = 3;
            solver.Solve();
        }
    }
}
