using System;

namespace Problem_54
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Problem 54");
            int start = Environment.TickCount;
            //clubs (♣), diamonds (♦), hearts (♥) and spades (♠)
            string h1 = "4D 6S 9H QH QC";
            string h2 = "3D 6D 7H QD QS";
            PokerHand playerOne = new(h1);
            PokerHand playerTwo = new(h2);
            Console.WriteLine($"Player one: {h1} - {playerOne.TheResult}, {playerOne.Value} points");
            Console.WriteLine($"Player two: {h2} - {playerTwo.TheResult}, {playerTwo.Value} points");
            if (playerOne.Value == playerTwo.Value)
            {
                // draw: compare cards
                Console.WriteLine($"Draw. Player {PokerHand.HighestHandOnDraw(playerOne, playerTwo)} wins with higest card!");
            }
            else if (playerOne.Value > playerTwo.Value)
            {
                Console.WriteLine($"Player One wins with {playerOne.TheResult.ToLower()}");
            }
            else
            {
                Console.WriteLine($"Player Two wins with {playerTwo.TheResult.ToLower()}");
            }



            Console.WriteLine($"Duration: {Environment.TickCount-start} ms");

        }
    }
}
