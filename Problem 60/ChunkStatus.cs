using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_60
{
    /// <summary>
    /// Enum representing the status of a chunckfile
    /// </summary>
    public enum ChunkStatus
    {
        New = 1,
        ProcessedAndFound = 2,
        ProcessedAndNotFound = 3
    }
}
