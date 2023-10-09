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
            GameLoop();
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
            //DrawBoard();
            //do
            //{
            var pieceInformation = GetMove();
            UpdateGameState(pieceInformation);
            Console.Clear();
            DrawBoard();
            //} while (true);
        }

        public void UpdateGameState(((int XPos, int YPos) position, PlayerType player, char pieceStyle) info)
        {
            GameBoard.GameGrid[info.position.XPos, info.position.YPos].UpdateState(info.player, info.pieceStyle);
        }
        public void DrawBoard()
        {
            GameLogic.ForLoop(GameBoard.GameGrid, (i, j, cell) =>
            {
                Console.Write($"[{(cell.PieceStyle)}]{(j == GameBoard.GridSizeY - 1 ? "\n" : "")}");
            });
        }
        private ((int XPos, int YPos), PlayerType Player, char PieceStyle) GetMove()
        {


            //if (ComputerMoveNow)
            //{
            ComputerMoveNow ^= true;
            return Comp.MakeMove();
            //}
            //else
            //{
            //    ComputerMoveNow ^= true;
            //    return (ConsoleIO.UserInteraction.GetPlayerMove());
            //}


        }

        public static void ForLoop(Piece[,] pieceArray, Action<int, int, Piece> action)
        {
            for (int i = 0; i <= pieceArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= pieceArray.GetUpperBound(1); j++)
                {
                    action(i, j, pieceArray[i, j]);
                }
            }
        }
    }
}
