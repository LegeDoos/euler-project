using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Problem_60
{
    class Problem60Solver
    {
        public Dictionary<int, HashSet<int>> RelationSets { get; set; }

        /// <summary>
        /// The number of nodes to solve the problem for
        /// </summary>
        public int NumberOfNodes { get; set; }

        /// <summary>
        /// The number of relations required between the numbers to find the solution
        /// </summary>
        private int NumberOfRelations
        {
            get { return NumberOfNodes - 1; }
        }

        /// <summary>
        /// Contains the currently found smallest sum
        /// </summary>
        public int MinSum { get; set; }

        /// <summary>
        /// Contains the list of prime numbers for analyzing
        /// </summary>
        private List<int> Primes { get; set; }

        /// <summary>
        /// The maximum prime number to use (for performance, increase if nu result found)
        /// </summary>
        public int MaxPrime { get; set; }

        /// <summary>
        /// The hashset with the lowest sum
        /// </summary>
        public HashSet<int> CurrentResult { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_primeNumbers">The helper list with primes to use</param>
        /// <param name="_resetRelations">On true, recalculate the relations from the primes</param>
        public Problem60Solver()
        {
            RelationSets = new Dictionary<int, HashSet<int>>();
            MinSum = Int32.MaxValue;

            PrimeNumbers primeNumbers;
            try
            {
                primeNumbers = PrimeNumbers.LoadFromFile($"{Directory.GetCurrentDirectory()}\\Primes.json");
                Primes = primeNumbers.Numbers;
            }
            catch (Exception)
            {
                // error loading prime numbers so generate
                primeNumbers = new();
                primeNumbers.CalculateTo = 100000000;
                primeNumbers.CalculatePrimeNumbers();
                primeNumbers.WriteToDisk($"{Directory.GetCurrentDirectory()}");
                Primes = primeNumbers.Numbers;
            }
        
            NumberOfNodes = 3; //default
        }

        /*
         * Problem 60
         * The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.
         * Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
         */

        public void Solve()
        {
            Console.WriteLine("Generate relations");
            int start = Environment.TickCount;

            if (File.Exists($"{Directory.GetCurrentDirectory()}\\RelationSets.json"))
            {
                string json = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\RelationSets.json");
                RelationSets = JsonSerializer.Deserialize<Dictionary<int, HashSet<int>>>(json);
            }
            else
            {
                // Create all sets for all primes to maxprime
                int i = 0;
                int cntHeader = Primes.IndexOf(Primes.First(p=>p>MaxPrime));
                Console.Clear();

                foreach (var leftPrime in Primes.Where(p => p<=MaxPrime))
                {
                    i++;

                    // determine relations
                    int j = 0;
                    int cntLines = Primes.Where(p => p > leftPrime && p <= MaxPrime).Count();
                    foreach (var rightPrime in Primes.Where(p => p > leftPrime && p <= MaxPrime))
                    {
                        j++;
                        if (j % 10 == 0)
                        {
                            Console.Write($"\rAnalyzing primes: prime {i}/{cntHeader} {j}/{cntLines}");
                        }
                        try
                        {
                            var option1 = Int32.Parse($"{leftPrime}{rightPrime}");
                            var option2 = Int32.Parse($"{rightPrime}{leftPrime}");
                            if (Primes.Exists(p => p == option1) && Primes.Exists(p => p == option2))
                            {
                                // valid relation so add
                                if (!RelationSets.ContainsKey(leftPrime))
                                {
                                    // create hashset
                                    HashSet<int> set = new()
                                    {
                                        leftPrime
                                    };
                                    RelationSets.Add(leftPrime, set);
                                }
                                RelationSets[leftPrime].Add(rightPrime);
                            }
                        }
                        catch (Exception)
                        {
                            // just skip, value is too large anyway
                        }
                    }
                }

                string json = JsonSerializer.Serialize<Dictionary<int, HashSet<int>>>(RelationSets);
                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\RelationSets.json", json);
            }

            Console.WriteLine($"Relations generated. Elapsed: {(Environment.TickCount - start)/1000} seconds");

            // Analyze data
            Console.WriteLine("Analyze relations");
            start = Environment.TickCount;

            // iterate all sets that have at least "NumberOfRelations" items
            foreach (var theSet in RelationSets.Where(s => s.Value.Count >= NumberOfRelations))
            {
                var path = new HashSet<int>
                {
                    theSet.Key //add first key
                };

                foreach (var currentKey in theSet.Value)
                {
                    // skip the first one
                    if (theSet.Key != currentKey)
                    {
                        // only recurse when possible result doesn't exceed minsum
                        if (theSet.Value.Sum() + (currentKey * (NumberOfNodes - 1)) <= MinSum)
                        {
                            IntersectRecursively(theSet.Value, currentKey, 1, path);
                        }
                    }
                }
            }

            Console.WriteLine($"Relations analyzed. Elapsed: {(Environment.TickCount - start)/1000} seconds");

            // print result
            if (CurrentResult != null)
            {
                Console.WriteLine($"Result with lowest sum: {string.Join(';', CurrentResult)} with sum {MinSum}");
            }
            Console.WriteLine("End of script");
            Console.ReadKey();
        }


        private void IntersectRecursively(IEnumerable<int> _sourceSet, int _targetPrime, int _level, HashSet<int> _path)
        {
            // set the level
            int currentLevel = _level + 1;
            int levelsToGo = NumberOfRelations - currentLevel;

            // create the current path
            HashSet<int> path = _path.ToHashSet();
            path.Add(_targetPrime);

            // get intersection:
            // intersection contains the relations thet needs to be examined that are left, except the already added noed to path
            if (!RelationSets.ContainsKey(_targetPrime))
            {
                return;
            }

            var intersection = _sourceSet.Intersect(RelationSets[_targetPrime]);

            // process the last item
            if (levelsToGo == 0)
            {
                // skip the current targetprime
                if (intersection.Count() > 1) 
                {
                    // last iteration so the intersection sets contains the last nodes
                    foreach (var node in intersection)
                    {
                        if (node != _targetPrime)
                        {
                            // there is a result!
                            Console.WriteLine($"\rResult found {string.Join(";", path)};{node}");

                            var sum = path.Sum() + node;
                            if (sum < MinSum)
                            {
                                MinSum = sum;
                                CurrentResult = path.ToHashSet();
                                CurrentResult.Add(node);
                            }
                        }
                    }
                }
                return;
            }

            // if the number of levels to go > the number of relations left in the intersection that stop iterating. -1 for the intersection contains the current node
            if (intersection.Count() -1 < levelsToGo)
            {
                // Less items in interesection than needed items for solution
                return; //todo: wat return?
            }

            // proceed
            foreach (var primeNumber in intersection)
            {
                if (primeNumber > _targetPrime)
                {
                    // check if totalsum will exceed minsum
                    if (path.Sum() + ((NumberOfNodes - currentLevel) * primeNumber) <= MinSum)
                    {
                        IntersectRecursively(intersection, primeNumber, currentLevel, path);
                    }
                }
            }
        }
    }
}
