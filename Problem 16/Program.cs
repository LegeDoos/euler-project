using System;
using System.Numerics;

namespace Problem_16
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 16");
            int start = Environment.TickCount;

            int power = 1000;
            BigInteger result = 2;
            for (int i = 1; i < power; i++)
            {
                result *= 2;
            }
            var originalValue = result;

            BigInteger sum = 0;
            do
            {
                var digit = result % 10;
                sum += digit;
                result = result - digit;
                result /= 10;
            } while (result >= 10);
            sum += result;

            Console.WriteLine($"Result: 2 pow {power} = {originalValue}. Sum of digits = {sum}");
            Console.WriteLine($"Duration {Environment.TickCount - start} ms");
        }
    }
}
