using TicTakToe.Game;

namespace TicTakToe.Logic
{
    internal class GameLogic
    {
        private PlayerType player = PlayerType.Player;
        private PlayerType computer = PlayerType.Computer;
        private char PlayerPiece;
        private char ComputerPiece;
        private Board GameBoard;
        public GameLogic(char playerPiece, Board gameBoard)
        {
            ConsoleIO.ConsoleIO.Welcome();
            PlayerPiece = ConsoleIO.ConsoleIO.GetPlayerIcon("Would you like to play as 'X' or 'O': ", "xo");

            var boardSize = ConsoleIO.ConsoleIO.GetBoardSize();

            GameBoard = InitializeGame();
        }
        public Board InitializeGame()
        {

            return new Board(boardSize, boardSize);
        }
    }
}
