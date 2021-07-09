using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_18
{
    class PyramidItem
    {
        /// <summary>
        /// The value of the item
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Max total of underlying nodes including this node
        /// </summary>
        public long MaxTotal { get; set; } = -1;
    }

    /// <summary>
    /// Implementation of a balanced binary tree in a List
    /// </summary>
    class Pyramid
    {
        /// <summary>
        /// Dictionary holding the pyramid items <"x,y", PyramidItem>
        /// </summary>
        private Dictionary<string, PyramidItem> ThePyramid { get; set; }

        /// <summary>
        /// Last used X value
        /// </summary>
        private int LastX { get; set; } = 0;
        /// <summary>
        /// Last used Y value
        /// </summary>
        private int LastY { get; set; } = 0;

        public int Lookup { get; set; } = 0;
        public int Calculate { get; set; } = 0;

        public Pyramid()
        {
            ThePyramid = new();
        }

        /// <summary>
        /// Add an item to the first available location in the tree
        /// </summary>
        /// <param name="_value">The value to add</param>
        public void Add(int _value)
        {
            //determine last position
            if (LastX == LastY)
            {
                LastX++;
                LastY = 1;
            }
            else 
            {
                LastY++;
            }
            ThePyramid.Add(IndexToKey(LastX, LastY), new() { Value = _value });
        }

        /// <summary>
        /// Create the index for the key
        /// </summary>
        /// <param name="_key">The key</param>
        /// <returns>An array representing the key a,b</returns>
        private static int[] KeyToIndex(string _key) => Array.ConvertAll(_key.Split(","), int.Parse);

        /// <summary>
        /// Create the key for an index
        /// </summary>
        /// <param name="_x">x value</param>
        /// <param name="_y">y value</param>
        /// <returns>a sting containing the key</returns>
        private static string IndexToKey(int _x, int _y) => $"{_x},{_y}";

        /// <summary>
        /// Add items to the first available location in the tree
        /// </summary>
        /// <param name="_values">An int array with values</param>
        public void Add(int[] _values)
        {
            foreach (var val in _values)
            {
                Add(val);
            }
        }

        /// <summary>
        /// Get the value of a position in the tree
        /// </summary>
        /// <param name="_index">The index to get the item for starting at 1.</param>
        /// <returns>The value of the item on the given position. -1 in case of invalid index</returns>
        public int GetItemValue(string _key)
        {
            if (ThePyramid.ContainsKey(_key))
                return ThePyramid[_key].Value;
            else
                return -1;
        }

        /// <summary>
        /// Get a list containing the indexes of the childs of an item. Check existence of key before calling the method!
        /// </summary>
        /// <param name="_key">The index of the item. Index starting at 1.</param>
        /// <returns>A list containing the positions of the chilren.</returns>
        public static List<string> GetChildKeys(string _key)
        {
            List<string> result = new();
            var indexes = KeyToIndex(_key);
            string left = IndexToKey(indexes[0] + 1, indexes[1]);
            string right = IndexToKey(indexes[0] + 1, indexes[1] + 1);
            result.Add(left);
            result.Add(right);
            return result;
        }


        /// <summary>
        /// Get the max total of the values from a given index
        /// </summary>
        /// <param name="_key">Index starting at 1</param>
        /// <returns></returns>
        public long GetMaxTotal(string _key = "1,1")
        {
            if (ThePyramid.TryGetValue(_key, out PyramidItem theItem))
            {
                if (theItem.MaxTotal == -1)
                {
                    Calculate++;
                    long max = 0;
                    //Calculate and return
                    foreach (var item in GetChildKeys(_key))
                    {
                        max = Math.Max(max, this.GetMaxTotal(item));
                    }
                    theItem.MaxTotal = theItem.Value + max;
                    return theItem.MaxTotal;
                }
                else
                {
                    Lookup++;
                    return theItem.MaxTotal;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
