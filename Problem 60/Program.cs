using System;
using System.Collections.Generic;

namespace Problem_60
{
    class Program
    {
        static void Main()
        {
            Problem60Solver solver = new();
            solver.NumberOfNodes = 4;
            solver.MaxPrime = 20000; // try not to get over int32 range
            solver.Solve();
        }
    }
}
