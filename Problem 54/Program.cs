using System;
using System.IO;

namespace Problem_54
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Problem 54");
            int start = Environment.TickCount;
            int playerOneWins = 0;

            //clubs (♣), diamonds (♦), hearts (♥) and spades (♠)
            //string h1 = "4D 6S 9H QH QC";
            //string h2 = "3D 6D 7H QD QS";

            string line;
            using (StreamReader file = new StreamReader($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}\\p054_poker.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    // validate line
                    string h1 = line.Substring(0, 14);
                    string h2 = line.Substring(15, 14);
                    PokerHand playerOne = new(h1);
                    PokerHand playerTwo = new(h2);
                    Console.WriteLine($"Player one: {h1} - {playerOne.TheResult}, {playerOne.Value} points");
                    Console.WriteLine($"Player two: {h2} - {playerTwo.TheResult}, {playerTwo.Value} points");
                    if (playerOne.Value == playerTwo.Value)
                    {
                        // draw: compare cards
                        int highest = PokerHand.HighestHandOnDraw(playerOne, playerTwo);
                        Console.WriteLine($"Draw. Player {highest} wins with higest card!");
                        if (highest == 1)
                        {
                            playerOneWins++;
                        }
                    }
                    else if (playerOne.Value > playerTwo.Value)
                    {
                        Console.WriteLine($"Player One wins with {playerOne.TheResult.ToLower()}");
                        playerOneWins++;
                    }
                    else
                    {
                        Console.WriteLine($"Player Two wins with {playerTwo.TheResult.ToLower()}");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Result: player one wins {playerOneWins} times");
            Console.WriteLine($"Duration: {Environment.TickCount-start} ms");
        }
    }
}
