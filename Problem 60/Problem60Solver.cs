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
        /// List representing the relations between primes that meet the criteria
        /// </summary>
        private List<Relation> Relations { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_primeNumbers"></param>
        public Problem60Solver(PrimeNumbers _primeNumbers)
        {
            PrimeNumbers = _primeNumbers.Numbers;
            Relations = new();
        }

        /*
         * Problem 60
         * The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
         * Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
         */

        public List<int> Solve()
        {
            List<int> result;

            // determine prime numbers that meet the terms
            foreach (var number in PrimeNumbers)
            {
                if (AnalyzePrime(number))
                {
                    result = AnalyzeResult();
                    if (result != null)
                    {
                        // finished
                        return result;
                    }
                }

            }


            

            return null;
        }

        private List<int> AnalyzeResult()
        {
            // there have to be at least 5 nodes with 4 incoming and 4 outgoing relations
            var fromCount = from r in Relations
                       group r.From by r.From into c
                       where c.Count() >= 4
                       select new { From = c.Key, Count = c.Count() };

            var toCount = from r in Relations
                            group r.To by r.To into c
                            where c.Count() >= 4
                            select new { To = c.Key, Count = c.Count() };

            if (fromCount.Count() > 2 && toCount.Count() > 2 )
            {
                /*var join = fromCount.Join(toCount,
                                            f => f.From,
                                            t => t.To,
                                            (f, t) => new { From = f.From, To = t.To }
                    );*/
            }
            
            return null;
        }

        /// <summary>
        /// Analyze a single prime number and add to the relations if it's a "special one"
        /// </summary>
        /// <param name="_prime">The number to analyze</param>
        /// <returns>True if a relation is added</returns>
        private bool AnalyzePrime(int _prime)
        {
            int factor = 10;
            int primeLeft = _prime / factor;
            int primeRight = _prime % factor;
            bool retVal = false;

            while (primeLeft > 0)
            {
                // are both different primes?
                if (primeLeft != primeRight && PrimeNumbers.Exists(p => p.Equals(primeLeft)) && PrimeNumbers.Exists(p => p.Equals(primeRight)))
                {
                    // are they primes in different order?
                    int newPrime = Int32.Parse($"{primeRight}{primeLeft}");
                    if (PrimeNumbers.Exists(p => p.Equals(newPrime)))
                    {
                        // add to relations if not exists
                        if (!(Relations.Exists(r => r.From.Equals(primeLeft)) && Relations.Exists(r => r.To.Equals(primeRight))))
                        {
                            Relations.Add(new Relation() { From = primeLeft, To = primeRight });
                            retVal = true;
                        }
                        if (!(Relations.Exists(r => r.From.Equals(primeRight)) && Relations.Exists(r => r.To.Equals(primeLeft))))
                        {
                            Relations.Add(new Relation() { From = primeRight, To = primeLeft });
                            retVal = true;
                        }
                    }
                }
                factor = factor * 10;
                primeLeft = _prime / factor;
                primeRight = _prime % factor;
            }
            return retVal;
        }
    }
}
