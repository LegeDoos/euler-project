using System;

namespace Problem_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 9");
            int a = 1;
            int b = 2;
            int c = 1000 - b - a;
            bool found = false;
            
            while (c > 0 && !found)
            {
                while (b < c && !found)
                {
                    found = found || Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2);
                    b = !found ? b+1 : b;
                    c = 1000 - b - a;
                }
                a = !found ? a + 1 : a;
                b = !found ? a + 1 : b;
                c = 1000 - b - a;
            }
            Console.WriteLine($"Result found = {found}; Result = {a}*{b}*{c}={a*b*c}");
        }
    }
}
