using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_60
{
    class Problem60Solver
    {
        /// <summary>
        /// Contains the list of prime numbers to analyze
        /// </summary>
        public List<int> PrimeNumbers { get; private set; }

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_primeNumbers"></param>
        public Problem60Solver(PrimeNumbers _primeNumbers)
        {
            PrimeNumbers = _primeNumbers.Numbers;

        }

        /*
         * Problem 60
         * The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
         * Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
         */

        public List<int> Solve()
        {
            List<int> result = new();

            // determine prime numbers that meet the terms

            // Create the dictionary to monitor result, initialize to true
            Dictionary<int, bool> numbers = new();
            foreach (var number in PrimeNumbers)
            {
                numbers.Add(number, true);
            }


            return result;
        }
    }
}
