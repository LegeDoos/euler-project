using System;
using System.Collections.Generic;

namespace Problem_17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Problem 17");
            Dictionary<int, string> words = new();
            words.Add(1, "one");
            words.Add(2, "two");
            words.Add(3, "three");
            words.Add(4, "four");
            words.Add(5, "five");
            words.Add(6, "six");
            words.Add(7, "seven");
            words.Add(8, "eight");
            words.Add(9, "nine");
            words.Add(10, "ten");
            words.Add(11, "eleven");
            words.Add(12, "twelve");
            words.Add(13, "thirteen");
            words.Add(14, "fourteen");
            words.Add(15, "fifteen");
            words.Add(16, "sixteen");
            words.Add(17, "zeventeen");
            words.Add(18, "eighteen");
            words.Add(19, "nineteen");
            words.Add(20, "twenty");
            words.Add(30, "thrity");
            words.Add(40, "forty");
            words.Add(50, "fifty");
            words.Add(60, "sixty");
            words.Add(70, "seventy");
            words.Add(80, "eighty");
            words.Add(90, "ninety");
            words.Add(1000, $"one thousand");

            for (int i = 21; i < 100; i++)
            {
                var d = i % 10;
                var t = i - d;
                if (!words.ContainsKey(i))
                {
                    words.Add(i, $"{words[t]}-{words[d]}");
                }
            }

            for (int i = 100; i < 1000; i++)
            {
                if (i % 100 == 0)
                {
                    var h = i / 100;
                    if (!words.ContainsKey(i))
                    {
                        words.Add(i, $"{words[h]} hundred");
                    }
                } else
                {
                    var d = i % 100;
                    var h = i - d;
                    if (!words.ContainsKey(i))
                    {
                        words.Add(i, $"{words[h]} and {words[d]}");
                    }
                }
            }

            int sum = 0;
            for (int i = 1; i <= 1000; i++)
            {
                var word = words[i];
                var len = word.Replace(" ", "").Replace("-", "").Length;
                sum += len;
                Console.WriteLine($"{word} -> length = {len}");
            }

            Console.WriteLine($"Result is {sum}");
        }

    }
}
