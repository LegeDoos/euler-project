using System;

namespace Problem_6
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                The sum of the squares of the first ten natural numbers is,
                The square of the sum of the first ten natural numbers is,
                Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is .

                Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
             */
            Console.WriteLine("Problem 6");
            int maxnaturalNumbers = 100;
            int sumOfSquares = 0;
            int sum = 0;
            for (int i = 1; i<= maxnaturalNumbers; i++)
            {
                sumOfSquares += i*i;
                sum += i;
            }
            int sumSquare = sum * sum;
            Console.WriteLine($"Result: difference between sum of squares and square sum is {sumSquare}-{sumOfSquares}={sumSquare- sumOfSquares}");

        }
    }
}
