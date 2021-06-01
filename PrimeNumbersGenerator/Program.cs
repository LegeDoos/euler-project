using System;
using System.IO;

namespace PrimeNumbersGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimeNumbers prim = new();
            prim.CalculateTo = 10000;
            prim.CalculatePrimeNumbers();

            //string result = String.Empty;
            //foreach (var n in prim.Numbers)
            //{
            //    result = $"{result};{n}";
            //}
            //Console.WriteLine(result);

            prim.WriteToDisk(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
        }
    }
}
