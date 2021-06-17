using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_4
{
    /*  
    A palindromic number reads the same both ways. The largest palindrome made from the product of two 2 - digit numbers is 9009 = 91 × 99.
    Find the largest palindrome made from the product of two 3 - digit numbers.
    */

    class PalendromicNumbers
    {
        /// <summary>
        /// Number of digits to find the palindrome product for
        /// </summary>
        public int NumberOfDigits { get; set; }

        /// <summary>
        /// Set with the numbers that made the product
        /// </summary>
        public HashSet<int> Numbersresult { get; private set; }

        /// <summary>
        /// The palandromic result of the numbers
        /// </summary>
        public int PalindromicProductResult { get; private set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public PalendromicNumbers()
        {
        }

        /// <summary>
        /// Find the numbers
        /// </summary>
        /// <returns>A set with the numbers</returns>
        public void FindNumbers()
        {
            // determine max
            int maxNumber = Int32.Parse("9".PadLeft(NumberOfDigits, '9'));

            // determing max product
            for (int i = maxNumber; i > 0; i--)
            {
                for (int j = maxNumber; j > 0; j--)
                {
                    if (IsPalendromic(i * j))
                    {
                        if (i * j > PalindromicProductResult)
                        {
                            PalindromicProductResult = i * j;
                            Numbersresult = new HashSet<int>() { i, j };
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if a number is palendromic
        /// </summary>
        /// <param name="_number"></param>
        /// <returns></returns>
        private bool IsPalendromic(int _number)
        {
            string forward = $"{_number}";
            char[] charArray = forward.ToCharArray();
            Array.Reverse(charArray);            
            string backward = new string(charArray);
            return forward.Equals(backward);
        }
    }
}
