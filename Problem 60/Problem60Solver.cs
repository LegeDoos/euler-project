using Facet.Combinatorics;
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
        /// The number of nodes to solve the problem for
        /// </summary>
        public int NumberOfNodes { get; set; }

        /// <summary>
        /// The number of relations required between the numbrs to find the solution
        /// </summary>
        private int NumberOfRelations {
            get { return NumberOfNodes - 1; }
        }

        /// <summary>
        /// Contains the list of prime numbers to analyze
        /// </summary>
        public List<int> PrimeNumbers { get; private set; }

        /// <summary>
        /// List representing the relations between primes that meet the criteria
        /// </summary>
        private List<Relation> Relations { get; set; }

        /// <summary>
        /// Sets of primes, represented by a string and the sum of that set (meets requirements or not)
        /// </summary>
        private List<Result> Sets { get; set; } //<"2,4,6,7", 78>

        /// <summary>
        /// The nodes that are used in the sets
        /// </summary>
        private List<int> Nodes { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_primeNumbers"></param>
        public Problem60Solver(PrimeNumbers _primeNumbers)
        {
            PrimeNumbers = _primeNumbers.Numbers;
            NumberOfNodes = 3; //default
            Relations = new();
            Nodes = new();
            Sets = new();
        }

        /*
         * Problem 60
         * The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
         * Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
         */

        public string Solve()
        {
            string result;

            // determine prime numbers that meet the terms
            foreach (var number in PrimeNumbers)
            {
                if (AnalyzePrime(number))
                {
                    if (AnalyzeResult())
                    {
                        // finished
                        //return result; //haal laagste som uit de lijst en return
                    }
                }
            }

            return null;
        }

        private bool AnalyzeResult()
        {
            // there have to be at least 5 nodes with 4 incoming and 4 outgoing relations
            var fromCount = from r in Relations
                       group r.From by r.From into c
                       where c.Count() >= NumberOfRelations
                       select new { From = c.Key, Count = c.Count() };

            var toCount = from r in Relations
                            group r.To by r.To into c
                            where c.Count() >= NumberOfRelations
                            select new { To = c.Key, Count = c.Count() };

            if (fromCount.Count() >= NumberOfNodes && toCount.Count() >= NumberOfNodes )
            {
                // check for the nodes, they have to be the same

                var currentNodes = fromCount.Join(toCount,
                                            f => f.From,
                                            t => t.To,
                                            (f, t) => f.From);

                
                if (currentNodes.Count() >= NumberOfNodes)
                {
                    // the minimum requirements are met so we can pick the nodes and create the relations
                    Nodes = new();
                    foreach (var node in currentNodes)
                    {
                        if (!Nodes.Exists(n => n.Equals(node)))
                        {
                            Nodes.Add(node);
                        }
                    }
                    if (CheckRelations())
                    {
                        // success!
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Generate all combinations and check
        /// </summary>
        /// <returns>True on victory!</returns>
        private bool CheckRelations()
        {
            // create all combinations
            Combinations<int> combi = new Combinations<int>(Nodes, NumberOfNodes);
            foreach (var c in combi)
            {
                Sets.Add(new Result() { Key = string.Join(";", c) });
            }

    /*        List<int> ResultSet = null;
            string setKey;
            int setSum = 0;
            bool ret = true;

            if (_newNode == -1)
            {
                setSum = 0;
                // create initial group. This is exactly one set that meets the requirments
                foreach (var sourceNode in Nodes)
                {
                    setSum += sourceNode;
                    // each node has a relation with all other nodes. There are no one way relation so you only have to check From (or To)
                    foreach (var targetNode in Nodes.Where(n => n!=sourceNode))
                    {
                        if (!Relations.Exists(r => r.From.Equals(sourceNode) && r.To.Equals(targetNode)))
                        {
                            // no relation so fail
                            ret = false;
                        }
                    }
                }
                if (ret)
                {
                    setKey = String.Join(";", Nodes);
                    Sets.Add(setKey, setSum);
                }
            }
            else
            {

            }

            */
            return false;
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
                factor *= 10;
                primeLeft = _prime / factor;
                primeRight = _prime % factor;
            }
            return retVal;
        }
    }
}
