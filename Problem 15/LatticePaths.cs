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

        public Dictionary<string, int> StepsToEnd { get; private set; } // <"x,y", nStepsToEnd>




        public LatticePaths(int _size)
        {
            Width = _size;
            Height = _size;
            StepsToEnd = new();
        }

        public int Routes()
        {
            // end node
            int a = Width;
            int result = 0;
            while (a >= 0)
            {
                result = RoutesFromNode(a, a);
                a--;
            }
            return result; // last one was 0,0
        }
        

        private int RoutesFromNode(int _x, int _y)
        {
            int nRoutes = 0;

            if (_x == Width && _y == Height)
            {
                //end
                return 1;
            }

            // if not already processed and not at the end
            if (!StepsToEnd.TryGetValue($"{_x},{_y}", out nRoutes))
            {
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
            return nRoutes;
        }
    }
}