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
        /// The result meets the criteria 
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// The key of the set
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// The sum of the set
        /// </summary>
        public int Sum { get; set; }
    }
}
