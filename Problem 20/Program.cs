using System;
using System.Numerics;

namespace Problem_20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 20");
            
            int fact = 100;
            BigInteger result = fact;
            for (int i = fact-1; i > 0; i--)
            {
                result *= i;
            }
            
            BigInteger sum = 0;
            do
            {
                var digit = result % 10;
                sum += digit;
                result = result - digit;
                result /= 10;
            } while (result >= 10);
            sum += result;

            Console.WriteLine($"Result {sum}");
        }
    }
}
