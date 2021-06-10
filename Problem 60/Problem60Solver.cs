using Facet.Combinatorics;
using Problem_60.ExtensionMethods;
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
        private List<int> PrimeNumbers { get; set; }

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
        /// Reset and recalculate the relations
        /// </summary>
        private bool ResetRelations { get; set; }
        /// <summary>
        /// Reset and recalculate the sums
        /// </summary>
        private bool ResetSums { get; set; }

        /// <summary>
        /// Size of the list met items before relation calculation
        /// </summary>
        int sumsize = 5000000;
        /// <summary>
        /// Number of items in the list per file
        /// </summary>
        int filesize = 1000000;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_primeNumbers">The helper list with primes to use</param>
        /// <param name="_resetRelations">On true, recalculate the relations from the primes</param>
        /// <param name="_resetSums">On true, recalculate the sums files with the primes</param>
        public Problem60Solver(PrimeNumbers _primeNumbers, bool _resetRelations = false, bool _resetSums = false)
        {
            PrimeNumbers = _primeNumbers.Numbers;
            NumberOfNodes = 3; //default

            Relations = new();
            ResetRelations = _resetRelations;
            ResetSums = _resetSums;

            Nodes = new();
            
            Sets = new();
        }

        /*
         * Problem 60
         * The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
         * Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
         */

        public void Solve()
        {
            // determine prime numbers that meet the terms
            if (File.Exists($"{Directory.GetCurrentDirectory()}\\Relations.json") && !ResetRelations)
            {
                string json = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Relations.json");
                Relations = JsonSerializer.Deserialize<List<Relation>>(json);
            }
            else
            {
                foreach (var number in PrimeNumbers)
                {
                    AnalyzePrime(number);
                }
                string json = JsonSerializer.Serialize<List<Relation>>(Relations);
                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Relations.json", json);
            }

            // Analyze the numbers and create the nodes that are relevant
            CreateNodes();

            // Generate relations and check
            CreateRelations();

            // Get ordered list of relations (first 1000000)
            // not effective: Sets = GetTopSum();
            // store for debug
            // File.WriteAllText($"{Directory.GetCurrentDirectory()}\\TopSum.json", JsonSerializer.Serialize<List<Result>>(Sets));

            CheckRelations();

            // Check result
            foreach (var set in Sets.OrderBy(s => s.s))
            {
                Console.WriteLine($"Found: {set.k}");
            }
            Console.WriteLine("End of script");
            Console.ReadKey();

        }


        /// <summary>
        /// Create all combinations of nodes and split in files of 1000000 relations
        /// </summary>
        private void CreateRelations()
        {
            if (ResetSums)
            {
                // delete the files
                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "Sums*.json"))
                {
                    File.Delete(file);
                }

                // recreate the combinations and files
                Combinations<int> combi = new Combinations<int>(Nodes, NumberOfNodes);

                //calculate sum per combi to start solving from lowest to highest
                var splits = combi.Split(filesize);
               // int count = (combi.Count() / filesize) + 1;
                int part = 1;

                foreach (var split in splits)
                {
                    Console.WriteLine($"Calculate sums for part {part}");
                    CalculateAndStoreSum(split, part);
                    part++;
                }
            }
        }

        /// <summary>
        /// Analyze the prime numbers and look for the prime numbers the terms
        /// </summary>
        /// <returns>True when the group with the lowest sum is found</returns>
        private void CreateNodes()
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
                    Console.WriteLine("Creating nodes...");
                    // the minimum requirements are met so we can pick the nodes and create the relations
                    foreach (var node in currentNodes)
                    {
                        if (!Nodes.Exists(n => n.Equals(node)) && node <= MaxPrime) // add a max prime to reduce calculating time
                        {
                            Nodes.Add(node);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check all relations for a file
        /// </summary>
        private void CheckRelations()
        {
            Console.WriteLine("Check relations");

            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "Sums*.json"))
            {

                Console.Clear();
                Console.WriteLine("Check relations");
                Console.WriteLine($"Calculate relations from file {file}");

                List<Result> currentList = JsonSerializer.Deserialize<List<Result>>(File.ReadAllText(file));

                int cnt = currentList.Count();
                int current = 0;
                foreach (var node in currentList)
                {
                    current++;
                    if (current % 10000 == 0)
                    {
                        var percent = current * 100.0 / cnt;
                        Console.Write($"\rProgress: {percent}% {"x".PadRight(current*100/cnt, 'x')}{"_".PadLeft(100 - (current * 100 / cnt), '_')}");
                    }
                    List<int> values = node.k.Split(";").Select(Int32.Parse).ToList();
                    node.l = true;
                    foreach (var srcNode in values)
                    {
                        foreach (var trgtNode in values)
                        {
                            if (srcNode < trgtNode)
                            {
                                if (!Relations.Exists(r => r.From.Equals(srcNode) && r.To.Equals(trgtNode)))
                                {
                                    node.l = false;
                                    break;
                                }
                            }
                        }
                        if (!node.l)
                            break;
                    }
                    if (node.l)
                    {
                        // found one!
                        Sets.Add(node);
                    }
                }

                // store
                File.WriteAllText($"{file}", JsonSerializer.Serialize<List<Result>>(currentList));
            }

        }

        /// <summary>
        /// Calculate the sums of a part and store to disk
        /// </summary>
        /// <param name="_part">The part containing combinations</param>
        /// <param name="_partNumber">The partnumber you are processing</param>
        private void CalculateAndStoreSum(IEnumerable<IEnumerable<int>> _part, int _partNumber)
        {
            List<Result> localSet = new List<Result>();
            // calculate sum
            foreach (var c in _part)
            {
                var k = string.Join(";", c.OrderBy(i => i).ToList());
                var r = new Result() { k = k, l = false, s = c.Sum() };
                localSet.Add(r);
            }
            // store file
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Sums_{_partNumber.ToString().PadLeft(10, '0')}.json", JsonSerializer.Serialize<List<Result>>(localSet));
        }

        /// <summary>
        /// Iterate all files and get the top x items
        /// </summary>
        /// <returns>The top x items of all the files</returns>
        private List<Result> GetTopSum()
        {
            List<Result> localList = new List<Result>();

            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "Sums*.json"))
            {
                Console.WriteLine($"Get sums from file {file}");
                List<Result> currentList = JsonSerializer.Deserialize<List<Result>>(File.ReadAllText(file));
                localList.AddRange(currentList);

                localList = localList.OrderBy(item => item.s).ToList();

                if (localList.Count > sumsize)
                    localList.RemoveRange(sumsize, localList.Count - sumsize);

                Console.WriteLine($"Last sum in the list: {localList.Last().s}");
            }

            return localList;
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
