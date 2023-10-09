﻿using TicTakToe.Logic.Enums;

namespace TicTakToe.ConsoleIO
{
    internal static class UserInteraction
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
        }

        public static int GetBoardSize()
        {

            var boardSize = Validation.GetInteger("Enter the size of the game of Tic Tac Toe: ", 3);

            return boardSize;
        }
        public static char GetPlayerIcon(string prompt, string allowedChars)
        {
            var playerIcon = Validation.GetCharFromSelection(prompt, allowedChars);
            return playerIcon;
        }
        public static MoveResult GetPlayerMove()
        {
            return MoveResult.Denied;
        }

        public static void Goodbye()
        {
            Console.WriteLine("\n=========================================================================");
            Console.WriteLine("\tThese are the game stats: " +
                "\n\tGames Played: " +
                "\n\tGames Won: " +
                "\n\tLeast amount of draws for one game: " +
                "\n\tMaximum amount of draws for one game: ");
            Console.WriteLine("========================================================================= ");
            Console.WriteLine("Thanks for playing Tic Tac Toe!");

        }
    }
}
