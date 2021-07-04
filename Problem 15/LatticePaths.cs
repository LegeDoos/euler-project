using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_15
{
    /// <summary>
    /// Helper class to determine all lattice paths
    /// </summary>
    class LatticePaths
    {
        /// <summary>
        /// Width of the grid
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Height of the grid
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Dictionary containing the steps to the end for a node:
        /// <"x,y", nStepsToEnd>
        /// </summary>
        public Dictionary<string, long> StepsToEnd { get; private set; }
        /// <summary>
        /// Count number of lookups
        /// </summary>
        public int Lookup { get; private set; }
        /// <summary>
        /// Count number of calculates
        /// </summary>
        public int Calculate { get; private set; }


        public LatticePaths(int _size)
        {
            Width = _size;
            Height = _size;
            StepsToEnd = new();
            StepsToEnd.Add($"{Width},{Height}", 1);
            Calculate = 1;
        }

        public long Routes()
        {
            // end node
            int a = Width;
            long result = 0;
            while (a >= 0)
            {
                result = RoutesFromNode(a, a);
                a--;
            }
            return result; // last one was 0,0
        }
        
        private long RoutesFromNode(int _x, int _y)
        {
            // if not already processed and not at the end
            if (!StepsToEnd.TryGetValue($"{_x},{_y}", out long nRoutes))
            {
                Calculate++;
                // next x
                if (_x < Width)
                {
                    nRoutes += RoutesFromNode(_x + 1, _y); // add one to the left
                }
                // next y
                if (_y < Height)
                {
                    nRoutes += RoutesFromNode(_x, _y + 1); // add one down
                }
                StepsToEnd.Add($"{_x},{_y}", nRoutes);
            }
            else
            {
                Lookup++;
            }
            return nRoutes;
        }

        public void ShowRoutes(int _x, int _y, int _pad = 5)
        {
            for (int x = _x; x <= Width; x++)
            {
                string line = "";
                for (int y = _y; y <= Height; y++)
                {
                    line = $"{line} {StepsToEnd[$"{x},{y}"].ToString().PadLeft(_pad, ' ')}";
                }
                Console.WriteLine(line);
            }
        }
    }
}