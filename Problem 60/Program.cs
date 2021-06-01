using System;
using System.IO;

namespace Problem_60
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimeNumbers prim = PrimeNumbers.LoadFromFile($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\PrimeNumbers_10000.json");
            Problem60Solver solver = new Problem60Solver(prim);
            solver.Solve();
        }
    }
}
