using System;
using System.Collections.Generic;

namespace Problem_15
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Problem 15");
            int start = Environment.TickCount;
            
            LatticePaths paths = new(20);
            Console.WriteLine($"Result: number of routes: {paths.Routes()} (Lookup = {paths.Lookup} Calculate = {paths.Calculate})");
            Console.WriteLine($"({Environment.TickCount - start} ms)");
            //paths.ShowRoutes(0, 0, 10);
        }


    

    }
}
