using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectEuler.Helpers
{
    /// <summary>
    /// Calculate and store prime numbers
    /// </summary>
    class PrimeNumbers
    {
        /// <summary>
        /// The list with calculated prime numbers
        /// </summary>
        public List<Int64> Numbers { get; set; }
        /// <summary>
        /// The upper range to calculate the numbers to, default 10
        /// </summary>
        public Int64 CalculateTo { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public PrimeNumbers()
        {
            Numbers = new();
        }

        /// <summary>
        /// Calculate the prime numbers from 0 to CalculateTo
        /// </summary>
        public void CalculatePrimeNumbers(bool _feedback = false)
        {
            // Dictionary with all numbers
            Dictionary<Int64, bool> allNumbers = new(); // <the number, is prime number>

            if (_feedback)
                Console.WriteLine("Create primenumbers...");

            // Add all odd numbers to dictionary
            allNumbers.Add(2, true);
            for (Int64 i = 1; i <= CalculateTo; i++)
            {
                if (i % 2 != 0)
                {
                    allNumbers.Add(i, true);
                }
                if (i % 10000 == 0 && _feedback)
                    Console.Write($"\rCreate prime numbers: Add odd numbers to list {i/(CalculateTo*1.0)*100}%".PadRight(100));
            }

            if (_feedback)
                Console.WriteLine();

            // 1 is not a prime number
            allNumbers[1] = false;
            Int64 pointer = 3; // start with prime number 3 
            while (pointer <= CalculateTo)
            {
                if (_feedback)
                    Console.Write($"\rCreate prime numbers: current number {pointer}");


                Int64 multiple = 2; // first one is the prime number
                while (pointer * multiple <= CalculateTo)
                {
                    // all multiples of pointer are not a prime number
                    if (allNumbers.ContainsKey(pointer * multiple))
                    { 
                        allNumbers[pointer * multiple] = false;
                    }
                    multiple++;
                }
                // set next pointer (look for next prime)
                pointer++;
                while (pointer <= CalculateTo && allNumbers.ContainsKey(pointer) && allNumbers[pointer] == false)
                {
                    pointer++;
                }
            }

            if (_feedback)
                Console.WriteLine("\nCreate final list of prime numbers");

            // fill the result list
            foreach (var tuple in allNumbers)
            {
                if (tuple.Value == true)
                {
                    Numbers.Add(tuple.Key);
                }
            }  
        }

        /// <summary>
        /// Calculate the factors for a given number
        /// </summary>
        /// <param name="_number">The number to calculate the factor for</param>
        /// <returns>A hashset containing the factors</returns>
        public List<Int64> CalculateFactor(Int64 _number)
        {            
            return CalculateFactor(_number, 2);
        }

        /// <summary>
        /// Recursive method to calculate the factors for a given number
        /// </summary>
        /// <param name="_number">The given number</param>
        /// <param name="_currentFactor">The current factor</param>
        /// <returns></returns>
        private List<Int64> CalculateFactor(long _number, Int64 _currentFactor = 2)
        {
            List<Int64> result;

            foreach (var factor in Numbers.Where(p => p >= _currentFactor && p <= _number))
            {
                // check if devision is whole number 
                if ((_number % factor) == 0)
                {
                    if (_number == factor)
                    {
                        // in this case we are done
                        result = new() { factor };
                        return result;
                    }
                    else
                    {
                        // continue
                        result = new() { factor };
                        var sub = CalculateFactor(_number / factor, factor).ToList();
                        return result.Concat(sub).ToList();
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Load the prime numbers from file
        /// </summary>
        /// <param name="_fileName">the file to load</param>
        /// <returns>The object</returns>
        public static PrimeNumbers LoadFromFile(string _fileName)
        {
            if (string.IsNullOrEmpty(_fileName))
                throw new Exception("No filename!");
            if (!File.Exists(_fileName))
                throw new Exception("File does not exist!");

            string json = File.ReadAllText(_fileName);
            return JsonSerializer.Deserialize<PrimeNumbers>(json);
        }

        /// <summary>
        /// Store the file to disk
        /// </summary>
        /// <param name="_path">The path to store the file</param>
        /// <returns></returns>
        public Boolean Write(string _path)
        {
            if (string.IsNullOrEmpty(_path))
                return false;
            if (!Directory.Exists(_path))
                return false;
            
            try
            {
                string json = JsonSerializer.Serialize(this);
                File.WriteAllText($"{_path}\\Primes.json", json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
