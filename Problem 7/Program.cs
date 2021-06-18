using System;

namespace Problem_7
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
             What is the 10 001st prime number?
            */
            Console.WriteLine("Problem 7");
            PrimeNumbers p = new PrimeNumbers();
            p.CalculateTo = 1000000;
            p.CalculatePrimeNumbers(true);
            Console.WriteLine($"Result = {p.Numbers[10001-1]}");

        }
    }
}
