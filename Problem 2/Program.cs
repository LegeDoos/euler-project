using System;
using System.Collections.Generic;

namespace Problem_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 2");
            /*
                Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:
                1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
                By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
            */

            // generate fibonacci
            int max = 4000000;
            List<int> fibonacci = new();
            fibonacci.Add(1);
            fibonacci.Add(2);
            int i = 3;
            while (fibonacci[i - 3] + fibonacci[i - 2] <= max)
            {
                fibonacci.Add(fibonacci[i - 3] + fibonacci[i - 2]);
                i++;
            }
            Console.WriteLine($"Fibonacci: {string.Join(";", fibonacci)}");

            // calculate solution
            int sum = 0;
            foreach (var item in fibonacci)
            {
                if (item % 2 == 0)
                    sum += item;
            }
            Console.WriteLine($"Result: {sum}");
        }
    }
}
