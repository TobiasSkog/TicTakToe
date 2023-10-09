using TicTakToe.Game;
using TicTakToe.Logic.Enums;

namespace TicTakToe.Logic.Computer
{
    internal class ComputerLogic
    {
        private Board GameBoard { get; set; }
        private Board SolutionBoard { get; }
        private char ComputerPieceStyle { get; set; }
        private int NumberOfPiecesPlaced { get; set; }
        private PlayerType BigBrain = PlayerType.Computer;

        public ComputerLogic(Board board, char computerPieceStyle)
        {
            ComputerPieceStyle = computerPieceStyle;
            GameBoard = board;
            SolutionBoard = board;
            NumberOfPiecesPlaced = 0;
        }

        public MoveResult MakeMove()
        {
            GameLogic.ForLoop(SolutionBoard.GameGrid, (i, j, piece) =>
            {
                GetNeighbours(piece);
            });

            FindNextMove();

            return MoveResult.Success;
        }

        private void GetNeighbours(Piece piece)
        {
            var playerNeighbours = 0;
            var computerNeighbours = 0;
            List<(int, int)> dxdy = new()
            {
                ( -1, -1 ), ( -1, 0 ), ( -1, 1 ),
                (  0, -1 ),            (  0, 1 ),
                (  1, -1 ), (  1, 0 ), (  1, 1 ),
            };

            foreach (var (dx, dy) in dxdy)
            {
                var neighbourX = ((piece.Position.Item1 + dx + SolutionBoard.GridSizeX) % SolutionBoard.GridSizeX);
                var neighbourY = ((piece.Position.Item2 + dy + SolutionBoard.GridSizeY) % SolutionBoard.GridSizeY);
                var neighbourPiece = SolutionBoard.GameGrid[neighbourX, neighbourY];


                if (neighbourPiece.PieceState == PieceState.PlayerPlaced) playerNeighbours++;
                else if (neighbourPiece.PieceState == PieceState.ComputerPlaced) computerNeighbours++;
            }

            piece.UpdatePlayerNeighbours(playerNeighbours, computerNeighbours, BigBrain);
        }




        private void FindNextMove()
        {
            foreach (var piece in SolutionBoard.GameGrid)
            {
                if (piece.PieceState == PieceState.ComputerPlaced)
                {

                }
            }
        }
    }
}
