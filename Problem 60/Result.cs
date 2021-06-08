using Facet.Combinatorics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_60
{
    /// <summary>
    /// Represents a combination of nodes
    /// </summary>
    class Result
    {
        /// <summary>
        /// The result meets the criteria l = lucky you, success! 
        /// </summary>
        public bool l { get; set; }
        /// <summary>
        /// The key of the set k = key
        /// </summary>
        public string k { get; set; }
        /// <summary>
        /// The sum of the set s = Sum
        /// </summary>
        public int s { get; set; }
    }
}
