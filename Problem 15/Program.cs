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

            LatticePaths paths = new(10, 10);
            paths.CreateRoutes(0,0);

            foreach (var route in paths.Routes)
            {
                Console.WriteLine(route);
            }

            Console.WriteLine($"Result: number of routes: {paths.NumberOfRoutes}");
            Console.WriteLine($"({Environment.TickCount - start} ms)");
        }


    

    }
}
