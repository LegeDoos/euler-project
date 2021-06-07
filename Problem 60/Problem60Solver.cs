using Facet.Combinatorics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        /// The  maximum prime number to use (for performance, increase if nu result found)
        /// </summary>
        public int MaxPrime { get; set; }

        /// <summary>
        /// The number of relations required between the numbers to find the solution
        /// </summary>
        private int NumberOfRelations {
            get { return NumberOfNodes - 1; }
        }

        /// <summary>
        /// Contains the list of prime numbers for analyzing
        /// </summary>
        public List<int> PrimeNumbers { get; private set; }

        /// <summary>
        /// List representing the relations between primes that meet the criteria (concat in each order leads to prime number)
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
        /// <param name="_primeNumbers">The helper list with primes to use</param>
        /// <param name="_reset">On true, don't get cache files from disc</param>
        public Problem60Solver(PrimeNumbers _primeNumbers, bool _reset = false)
        {
            PrimeNumbers = _primeNumbers.Numbers;
            NumberOfNodes = 3; //default
            if (File.Exists($"{Directory.GetCurrentDirectory()}\\Relations.json") && !_reset)
            {
                string json = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Relations.json");
                Relations = JsonSerializer.Deserialize<List<Relation>>(json);
            }
            else
            { 
              Relations = new();
            }
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
            // determine prime numbers that meet the terms
            if (Relations.Count == 0)
            {
                foreach (var number in PrimeNumbers)
                {
                    AnalyzePrime(number);
                }
                string json = JsonSerializer.Serialize<List<Relation>>(Relations);
                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Relations.json", json);
            }

            // Analyze the numbers
            AnalyzeResult();

            // Return the result
            return Sets.Find(l => l.Success).Key;
        }

        /// <summary>
        /// Analyze the found prime numbers and relations and look for the group of primes that meet the terms
        /// </summary>
        /// <returns>True when the group with the lowest sum is found</returns>
        private bool AnalyzeResult()
        {
            // there have to be at least NumberOfNodes nodes with NumberOfRelations incoming and NumberOfRelations outgoing relations
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
                    Console.WriteLine("Analyzing result...");
                    // the minimum requirements are met so we can pick the nodes and create the relations
                    foreach (var node in currentNodes)
                    {
                        if (!Nodes.Exists(n => n.Equals(node)) && node <= MaxPrime) // add a max prime to reduce calculating time
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
            bool ret = false;
            // create all combinations
            Combinations<int> combi = new Combinations<int>(Nodes, NumberOfNodes);

            // initialize
            
            // calculate sum per combi to start solving from lowest to highest
            var cnt = combi.Count;
            var i = 0;
            foreach (var c in combi)
            {
                i++;
                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Calculate sums {i}/{cnt}");
                }
                var k = string.Join(";", c.OrderBy(i => i).ToList());
                var r = new Result() { Key = k, Success = false, Sum = c.Sum() };
                Sets.Add(r);
            }

            foreach (var r in Sets.OrderBy(s => s.Sum))
            {
                List<int> values = r.Key.Split(";").Select(Int32.Parse).ToList();
                r.Success = true;
                foreach (var srcNode in values)
                {
                    foreach (var trgtNode in values)
                    {
                        if (srcNode < trgtNode)
                        {
                            if (!Relations.Exists(r => r.From.Equals(srcNode) && r.To.Equals(trgtNode)))
                            {
                                r.Success = false;
                                break;
                            }
                        }
                    }
                    if (!r.Success)
                        break;
                }
                if (r.Success)
                    return true;
            }

   
            return false;
        }


        private void CalculateSum(List<List<int>> _part, int _partNumber)
        {
            List<Result> localSet = new List<Result>();
            // calculate sum
            foreach (var c in _part)
            {
                var k = string.Join(";", c.OrderBy(i => i).ToList());
                var r = new Result() { Key = k, Success = false, Sum = c.Sum() };
                localSet.Add(r);
            }
            // store file
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Sums_{_partNumber}.json", JsonSerializer.Serialize<List<Result>>(localSet));
        }

        /// <summary>
        /// Analyze a single prime number and add to the relations if it's a "special one"
        /// </summary>
        /// <param name="_prime">The number to analyze</param>
        /// <returns>True if a relation is added</returns>
        private bool AnalyzePrime(int _prime)
        {
            Console.WriteLine($"Analyzing prime {_prime}");
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
                    int checkPrime = Int32.Parse($"{primeLeft}{primeRight}"); //check for ones like 3019, with a zero
                    int newPrime = Int32.Parse($"{primeRight}{primeLeft}");
                    if (checkPrime.Equals(_prime) && PrimeNumbers.Exists(p => p.Equals(newPrime)))
                    {
                        // add to relations if not exists
                        if (!(Relations.Exists(r => r.From.Equals(primeLeft) && r.To.Equals(primeRight))))
                        {
                            Relations.Add(new Relation() { From = primeLeft, To = primeRight });
                            retVal = true;
                        }
                        if (!(Relations.Exists(r => r.From.Equals(primeRight) && r.To.Equals(primeLeft))))
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
