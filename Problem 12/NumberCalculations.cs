using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Helpers
{
    class NumberCalculations
    {
        private Dictionary<int, HashSet<int>> AllFactors { get; set; }

        public NumberCalculations()
        {
            AllFactors = new();
        }

        public HashSet<int> FactorsFast(int value)
        {
            HashSet<int> result;
            int divisor = value;
                      
            if (!AllFactors.TryGetValue(value, out result))
            {
                if (value == 1)
                {
                    result = new() { 1 };
                    AllFactors.Add(1, result);
                    return result;

                }
                if (result == null)
                {
                    //calculate and add
                    result = new() { value }; // add current value
                    do
                    {
                        divisor--;
                    } while (divisor > 0 && value % divisor != 0);
                    result = FactorsFast(divisor).Concat(result).ToHashSet();
                    AllFactors.Add(value, result);
                }
            }
            return result;
        }


        /// <summary>
        /// Calculate all posible factors for a given number
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A list containing the factors</returns>
        static internal List<int> Factors(int value)
        {
            List<int> result = new();
            for (int i = 1; i <= value; i++)
            {
                if (value % i == 0)
                {
                    // is is a valid factor
                    result.Add(i);
                }
            }
            return result;
        }

        /// <summary>
        /// Calculate the number of all posible factors for a given number
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The number of factors</returns>
        static internal int NumberOfFactors(int value)
        {
            int result = 0;
            for (int i = 1; i <= value; i++)
            {
                if (value % i == 0)
                {
                    // is is a valid factor
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Calculate the factors for a given number
        /// </summary>
        /// <param name="_number">The number to calculate the factor for</param>
        /// <returns>A List containing the factors</returns>
        public List<int> CalculateFactor(int _number)
        {
            return CalculateFactor(_number, 1);
        }



        /// <summary>
        /// Recursive method to calculate the factors that lead to a given number
        /// </summary>
        /// <param name="_number">The given number</param>
        /// <param name="_currentFactor">The current factor to evaluate</param>
        /// <returns></returns>
        private List<int> CalculateFactor(int _number, int _currentFactor)
        {
            List<int> result = new();
            int factor = _currentFactor;
            
            if (_number % factor == 0)
            {
                result.Add(factor);
                if (_number == factor)
                {
                    // in this case we are done
                    return result;
                }
                // calculate next factor
                int newNumber = _number / factor;
                while (factor <= newNumber) 
                {
                        factor++; // get next factor
                        var sub = CalculateFactor(newNumber, factor).ToList();
                        if (sub.Count > 0)
                            return result.Concat(sub).ToList();
                }
            }
            return result;
        }
    }
}
