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
        
        public int NumberOfRoutes { get; private set; }
        public List<string> Routes{ get; set; }

        public Dictionary<string, int> StepsFromNode { get; private set; } // <"x,y", nSteps>
                                                                           

        
        
        public LatticePaths(int _w, int _h)
        {
            Width = _w;
            Height = _h;
            Routes = new();
            StepsFromNode = new();
        }


        public void CreateRoutes(int _x, int _y, string _current = "")
        {
            if (_x == Width && _y == Height)
            {
                // done
                Routes.Add(_current);
                NumberOfRoutes++;
            }
            else
            {
                // next x
                if (_x < Width)
                {
                    CreateRoutes(_x+1, _y, $"{_current}L"); // add one to the left
                }
                // next y
                if (_y < Height)
                {
                    CreateRoutes(_x, _y+1, $"{_current}D"); // add one down
                }
            }

            if (_current is null)
            {
                throw new ArgumentNullException(nameof(_current));
            }
        }
    }
}
