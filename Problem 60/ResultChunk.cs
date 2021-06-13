using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_60
{
    /// <summary>
    /// Class representing a chunck of data (combinations of primes) to process
    /// </summary>
    class ResultChunk
    {
        /// <summary>
        /// Status of the chunk
        /// </summary>
        public ChunkStatus Status { get; set; }
        /// <summary>
        /// The combinations of this chunck
        /// </summary>
        public List<Result> Combinations { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public ResultChunk()
        {
            Status = ChunkStatus.New;
            Combinations = new();
        }
    }
}
