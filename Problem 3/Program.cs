using System;
using System.IO;

namespace Problem_3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                The prime factors of 13195 are 5, 7, 13 and 29.
                What is the largest prime factor of the number 600851475143 ?
            */
            Console.WriteLine("Problem 3");
            Int64 maxPrime = 1000000;
            Int64 number = 600851475143;

            // calculate primenumbers
            PrimeNumbers primeNumbers;
            try
            {
                primeNumbers = PrimeNumbers.LoadFromFile($"{Directory.GetCurrentDirectory()}\\Primes.json");
            }
            catch (Exception)
            {
                primeNumbers = new();
            }
            if (primeNumbers.CalculateTo != maxPrime)
            {
                primeNumbers = new();
                primeNumbers.CalculateTo = maxPrime;
                primeNumbers.CalculatePrimeNumbers(true);
                primeNumbers.Write(Directory.GetCurrentDirectory());
            }

            // calculate factor
            Console.WriteLine($"The factors for {number} are {string.Join(",", primeNumbers.CalculateFactor(number))}");
            Console.WriteLine("End of script!");
        }
    }
}
