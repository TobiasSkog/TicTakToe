using TicTakToe.Game;

namespace TicTakToe.ConsoleIO
{
    public static class UserInteraction
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
        public static string GetPlayerIcon(string prompt, string allowedChars)
        {
            var playerIcon = Validation.GetCharFromSelection(prompt, allowedChars);
            return playerIcon;
        }
        public static (int X, int Y) GetPlayerMove(string prompt, Board gameBoard)
        {
            var playerPosition = Validation.IsValidMove(prompt, gameBoard);
            return playerPosition;
        }
        public static bool PlayAgain(string prompt)
        {
            var answer = Validation.PlayAgain(prompt);
            return answer;
        }

        public static void Goodbye(GameStatistics stats)
        {
            Console.WriteLine("\n=======================================================");
            Console.WriteLine("\tThese are the game stats: " +
                $"\n\tGames Played: {stats.GamesPlayed}" +
                $"\n\tGames Won: {stats.GamesWon} " +
                $"\n\tLeast amount of draws for one game: {stats.LowestMoveCount}" +
                $"\n\tMaximum amount of draws for one game: {stats.HighestMoveCount}");
            Console.WriteLine("=======================================================");
            Console.WriteLine("Thanks for playing Tic Tac Toe!");

        }
    }
}
