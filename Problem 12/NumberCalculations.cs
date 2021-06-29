using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Helpers
{
    /// <summary>
    /// Helperclass for calculations on numbers that can be reused
    /// </summary>
    class NumberCalculations
    {
        /// <summary>
        /// Get the factors of a given number.
        /// Get all factors that the given number is divisible by to a natural number
        /// </summary>
        /// <param name="value">The number to calculate the factors for</param>
        /// <returns>An hashset with all the factors</returns>
        public static HashSet<int> GetAllFactors(int value)
        {
            HashSet<int> result = new();
            int currentFactor = 1;
            int currentResult = value / currentFactor;
            // when the currentfactor is < than the currentresult, you are done becuase the rest of the factors are already added (the results of the first half)
            while (currentFactor <= currentResult)
            {
                result.Add(currentFactor);
                result.Add(currentResult);
                currentFactor++;
                // find the next factor:
                while (currentFactor < value && value % currentFactor != 0)
                {
                    currentFactor++;
                }
                currentResult = value / currentFactor;
            }
            return result;
        }
    }
}
