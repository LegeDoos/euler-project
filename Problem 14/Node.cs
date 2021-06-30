using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_14
{
    /// <summary>
    /// A node in the list
    /// </summary>
    class Node
    {
        /// <summary>
        /// The number of items in the sequence until you reach 1
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// The next item in the sequence
        /// </summary>
        public long Next { get; set; }
    }
}
