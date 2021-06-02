using System;
using System.IO;

namespace Problem_60
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimeNumbers primes = new PrimeNumbers();
            primes.CalculateTo = 110000;
            primes.CalculatePrimeNumbers();
            Problem60Solver solver = new Problem60Solver(primes);
            solver.Solve();
        }
    }
}
