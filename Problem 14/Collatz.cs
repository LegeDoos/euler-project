using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Problem_14
{
    class Collatz
    {
        /// <summary>
        /// A dictionary representing the sequences, implemented as a linked list
        /// </summary>
        private Dictionary<long, Node> Sequence { get; set; }
        public Collatz()
        {
            Sequence = new();
            Sequence.Add(1, new() { Count = 1, Next = -1 });
        }

        /// <summary>
        /// Get the length of the sequence of the startingnumber
        /// </summary>
        /// <param name="startingNumber">The number to get the sequence length for</param>
        /// <returns>The sequence length</returns>
        internal int GetLength(long startingNumber)
        {
            // find in dictionary
            Node theNode;
            if (Sequence.TryGetValue(startingNumber, out theNode))
            {
                // found so return length
                return theNode.Count;
            }
            else
            {
                // create the sequence
                long newNumber;
                if (startingNumber % 2 == 0)
                {
                    newNumber = startingNumber / 2;
                }
                else
                {
                    newNumber = (3 * startingNumber) + 1;
                }

                var cnt = GetLength(newNumber);
                Sequence.Add(startingNumber, new() { Count =  cnt + 1, Next =  newNumber });
                return cnt + 1;
            }
        }

        /// <summary>
        /// Get the sequence for a number
        /// </summary>
        /// <param name="startingNumber">The number</param>
        /// <returns>A list with the sequence</returns>
        internal List<long> GetTerms(long startingNumber)
        {
            List<long> result = new();
            long index = startingNumber;

            // make sure the sequence exists
            if (GetLength(startingNumber) > 0)
            {
                while (index >= 1)
                {
                    result.Add(index);
                    index = Sequence[index].Next;
                }
            }
            return result;
        }

        internal void SaveToFileForFun()
        {
            string json = JsonSerializer.Serialize(Sequence);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\LinkedList.json", json);
        }
    }
}
