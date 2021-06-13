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
        /// The key of the set K = key
        /// </summary>
        public string K { get; set; }
        /// <summary>
        /// The sum of the set S = Sum
        /// When sum is added, the set has succesfull relations, otherwise sum = -1
        /// </summary>
        public int S { get; set; }
    }
}
