using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Problem_60
{
    /// <summary>
    /// Calculate and store prime numbers
    /// </summary>
    class PrimeNumbers
    {
        /// <summary>
        /// The list with calculated prime numbers
        /// </summary>
        public List<int> Numbers { get; set; }
        /// <summary>
        /// The upper range to calculate the numbers to, default 10
        /// </summary>
        public int CalculateTo { get; set; } = 10;
        /// <summary>
        /// Default constructor
        /// </summary>
        public PrimeNumbers()
        {
            Numbers = new();
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
        /// Calculate the prime numbers from 0 to CalculateTo
        /// </summary>
        public void CalculatePrimeNumbers()
        {
            // Dictionary with all numbers
            Dictionary<int, bool> allNumbers = new(); // <the number, is prime number>

            // Add all numbers to dictionary
            for (int i = 1; i <= CalculateTo; i++)
            {
                allNumbers.Add(i, true);
            }

            // 1 is not a prime number
            allNumbers[1] = false;

           
            int pointer = 2; // start with prime number 2
            while (pointer <= CalculateTo)
            {
                int multiple = 2; // first one is the prime number
                while (pointer * multiple <= CalculateTo)
                {
                    // all multiples of pointer are not a prime number
                    allNumbers[pointer * multiple] = false;
                    multiple++;
                }
                // set next pointer (look for next prime)
                pointer++;
                while (pointer <= CalculateTo && allNumbers[pointer] == false)
                {
                    pointer++;
                }
            }

            // fill the result list
            foreach (var tuple in allNumbers)
            {
                if (tuple.Value == true)
                {
                    Numbers.Add(tuple.Key);
                }
            }  
        }

        public Boolean WriteToDisk(string _path)
        {
            if (string.IsNullOrEmpty(_path))
                return false;
            if (!Directory.Exists(_path))
                return false;
            
            try
            {
                string json = JsonSerializer.Serialize(this);
                File.WriteAllText($"{_path}\\PrimeNumbers.json", json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
