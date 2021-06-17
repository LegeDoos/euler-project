using System;

namespace Problem_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 4");
            /*  
                A palindromic number reads the same both ways. The largest palindrome made from the product of two 2 - digit numbers is 9009 = 91 × 99.
                Find the largest palindrome made from the product of two 3 - digit numbers.
            */
            PalendromicNumbers palendromicNumbers = new();
            palendromicNumbers.NumberOfDigits = 4;
            palendromicNumbers.FindNumbers();
            
            Console.WriteLine($"Result for {palendromicNumbers.NumberOfDigits} digit(s) is {string.Join(",", palendromicNumbers.Numbersresult)} with largest palindrome {palendromicNumbers.PalindromicProductResult}");
        }
    }
}
