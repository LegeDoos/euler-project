using System;
using System.Collections.Generic;

namespace Problem_15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 15");
            int start = Environment.TickCount;

            LatticePaths paths = new(20);
            Console.WriteLine($"{Int32.MaxValue}");
            Console.WriteLine($"Result: number of routes: {paths.Routes()}");
            Console.WriteLine($"({Environment.TickCount - start} ms)");
        }


    

    }
}
