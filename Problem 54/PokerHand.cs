using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem_54
{
    class PokerHand
    {
        /// <summary>
        /// Represents the hand of cards
        /// </summary>
        public List<Card> TheHand { get; set; }
        /// <summary>
        /// The result of the hand represented by a string
        /// </summary>
        public string TheResult { get; private set; }
        /// <summary>
        /// The main value of the hand, e.g. Royal Flush = 1000 points
        /// </summary>
        public int Value { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_hand">String representation of the hand of cards</param>
        public PokerHand(string _hand)
        {
            TheHand = new();
            var items = _hand.Split(" ");
            foreach (var item in items)
            {
                TheHand.Add(new(item));
            }
            ValueHand();
        }
        /// <summary>
        /// Check for n pairs
        /// </summary>
        /// <param name="_n">The number of pairs to find</param>
        /// <returns>True if n pairs</returns>
        private bool IsNPair(int _n)
        {
            var groups = TheHand
                  .GroupBy(c => c.Value)
                  .Select(grp => new { key = grp.Key, total = grp.Count() });
            return groups.Where(g => g.total == 2).Count() == _n;
        }
        /// <summary>
        /// Checks for full house
        /// </summary>
        /// <returns>True on full house</returns>
        private bool IsFullHouse()
        {
            var groups = TheHand
                  .GroupBy(c => c.Value)
                  .Select(grp => new { key = grp.Key, total = grp.Count() });
            return groups.Where(g => g.total == 2).Count() == 1
                && groups.Where(g => g.total == 3).Count() == 1;
        }
        /// <summary>
        /// Are there n cards with the same value?
        /// </summary>
        /// <returns>True if n cards with the same value</returns>
        private bool IsNOfAKind(int _n)
        {
            return TheHand
                .GroupBy(c => c.Value)
                .Select(grp => new { key = grp.Key, total = grp.Count() })
                .Where(s => s.total == _n)
                .Count() == 1;
        }
        /// <summary>
        /// Are all cards of the same suit?
        /// </summary>
        /// <returns>True if all from the same suit</returns>
        private bool IsFlush()
        {
            return TheHand.GroupBy(c => c.Suit).Select(grp => grp.Key).Count() == 1;
        }
        /// <summary>
        /// Are all cards in a consecutive value?
        /// </summary>
        /// <returns>True if in consecutive value</returns>
        private bool IsStraight()
        {
            int last = -1;
            bool straight = true;
            foreach (var card in TheHand.OrderBy(c => c.Value).ToList())
            {
                if (last == -1)
                    last = card.Value;
                else
                {
                    last++;
                    if (last != card.Value)
                    {
                        straight = false;
                        break;
                    }
                }
            }
            return straight;
        }
        /// <summary>
        /// Get the highest card from the hand
        /// </summary>
        /// <returns>The highest card</returns>
        private Card HighestCard()
        {
            var high = TheHand.Max(c => c.Value);
            return TheHand.FirstOrDefault(c => c.Value == high);
        }
        /// <summary>
        /// Values a hand represented by a space seperated string
        /// </summary>
        /// <param name="_hand">space seperated string with cards</param>
        /// <returns><val of the hand, val of the high card></returns>
        private void ValueHand()
        {
            bool straigt = IsStraight();
            bool flush = IsFlush();
            Card highestCard = HighestCard();

            // check royal flush
            if (straigt && flush && highestCard.Value == 14)
            {
                Value = 1000;
                TheResult = "Royal flush!";
                return;
            }
            // check straight flush
            if (straigt && flush)
            {
                Value = 900 + highestCard.Value;
                TheResult = "Straight flush!";
                return;
            }
            // four of a kind
            if (IsNOfAKind(4))
            {
                var group = TheHand
                .GroupBy(c => c.Value)
                .Select(grp => new { key = grp.Key, total = grp.Count() })
                .Where(s => s.total == 4);
                int value = group.First().key;

                var otherCard = TheHand.Where(c => c.Value != value).Select(c => c).First();
                Value = 800 + value;
                TheResult = "Four of a kind!";
                return;
            }
            // full house
            if (IsFullHouse())
            {
                Value = 700 + highestCard.Value;
                TheResult = "Full house!";
                return;
            }
            // flush
            if (flush)
            {
                Value = 600 + highestCard.Value;
                TheResult= "Flush!";
                return;
            }
            // straight
            if (straigt)
            {
                Value = 500 + highestCard.Value;
                TheResult = "Straight!";
                return;
            }
            // three of a kind
            if (IsNOfAKind(3))
            {
                var group = TheHand
                .GroupBy(c => c.Value)
                .Select(grp => new { key = grp.Key, total = grp.Count() })
                .Where(s => s.total == 3);
                int value = group.First().key;

                Value = 400 + value;
                TheResult = "Three of a kind!";
                return;
            }
            // two pairs
            if (IsNPair(2))
            {
                var groups = TheHand
                   .GroupBy(c => c.Value)
                   .Select(grp => new { key = grp.Key, total = grp.Count() });
           
                var value = groups.Where(g => g.total == 2).OrderByDescending(g => g.key).First().key;
                Value = 300 + value;
                TheResult = "Two pairs!";
                return;
            }
            // one pair
            if (IsNPair(1))
            {
                var groups = TheHand
                   .GroupBy(c => c.Value)
                   .Select(grp => new { key = grp.Key, total = grp.Count() });

                var value = groups.Where(g => g.total == 2).OrderByDescending(g => g.key).First().key;
                Value = 200 + value;
                TheResult = "One pair!";
                return;
            }
            // high card
            Value = highestCard.Value;
            TheResult = "Highcard!";
        }
        /// <summary>
        /// On a drawm check for the higest card in hand
        /// </summary>
        /// <param name="_h1">hand 1</param>
        /// <param name="_h2">hand 2</param>
        /// <returns>1 of hand 1 wins, 2 if hand 2 wins/returns>
        public static int HighestHandOnDraw(PokerHand _h1, PokerHand _h2)
        {
            var hand1Enum = _h1.TheHand.OrderByDescending(c => c.Value).GetEnumerator();
            var hand2Enum = _h2.TheHand.OrderByDescending(c => c.Value).GetEnumerator();
            while (hand1Enum.MoveNext() && hand2Enum.MoveNext())
            {
                if (hand1Enum.Current.Value != hand2Enum.Current.Value)
                {
                    return hand1Enum.Current.Value > hand2Enum.Current.Value ? 1 : 2;
                }
            }
            return 0;
        }
    }
}
