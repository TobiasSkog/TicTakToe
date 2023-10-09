using TicTakToe.Game;
using TicTakToe.Logic.Computer;
using TicTakToe.Logic.Enums;

namespace TicTakToe.Logic
{
    internal class GameLogic
    {
        private PlayerType Player = PlayerType.Player;
        private PlayerType ComputerType = PlayerType.Computer;
        private ComputerLogic Comp;
        private int GameGridSize;
        private bool ComputerMoveNow;
        private char PlayerPiece;
        private char ComputerPiece;
        private Board GameBoard;
        public GameLogic()
        {
            InitializeGame();

        }
        public void InitializeGame()
        {
            PlayerPiece = ConsoleIO.UserInteraction.GetPlayerIcon("Would you like to play as 'X' or 'O': ", "xo");
            Console.WriteLine();
            ComputerPiece = (PlayerPiece == 'X') ? 'O' : 'X';
            ComputerMoveNow = ComputerPiece == 'O';

            GameGridSize = ConsoleIO.UserInteraction.GetBoardSize();
            GameBoard = new Board(GameGridSize, GameGridSize);
            Comp = new ComputerLogic(GameBoard, ComputerPiece);
        }

        private void GameLoop()
        {
            do
            {
                GetMove();

            } while (true);
        }

        private void GetMove()
        {
            var move = MoveResult.Denied;

            do
            {

                if (ComputerMoveNow)
                {
                    move = Comp.MakeMove();

                }
                else
                {
                    move = ConsoleIO.UserInteraction.GetPlayerMove();
                }
            } while (move == MoveResult.Denied);

        }

        public static void ForLoop(Piece[,] pieceArray, Action<int, int, Piece> action)
        {

            for (int i = 0; i < pieceArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j < pieceArray.GetUpperBound(1); j++)
                {
                    action(i, j, pieceArray[i, j]);
                }
            }
        }
    }
}
