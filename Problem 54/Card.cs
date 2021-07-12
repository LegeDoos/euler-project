using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_54
{
    /// <summary>
    /// Represents a card
    /// </summary>
    class Card
    {
        /// <summary>
        /// String representing the card (e.g. KD is Kind of Diamonds)
        /// </summary>
        public string Representation { get; private set; }
        /// <summary>
        /// The value of a card from 2 to ace = 14
        /// </summary>
        public int Value { get; private set; }
        /// <summary>
        /// The suit of the card
        /// </summary>
        public string Suit { get; private set; }

        /// <summary>
        /// Contstructor
        /// </summary>
        /// <param name="_representation"></param>
        public Card(string _representation)
        {
            Representation = _representation;
            Suit = Representation.Substring(Representation.Length - 1, 1);
            string val = Representation.Substring(0, Representation.Length == 2 ? 1 : 2);
            switch (val)
            {
                case "T":
                    val = "10";
                    break;
                case "J": 
                    val = "11";
                    break;
                case "Q": 
                    val = "12";
                    break;
                case "K": 
                    val = "13";
                    break;
                case "A": 
                    val = "14";
                    break;
                default:
                    break;
            }
            Value = Int32.Parse(val);
        }
    }
}
