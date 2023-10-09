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

        public ((int XPos, int YPos), PlayerType Player, char PieceStyle) MakeMove()
        {
            var movePosition = FindBestMove();
            return (movePosition, BigBrain, ComputerPieceStyle);
        }
        //private MoveResult ComputerMakeMove((int XPos, int YPos) position)
        //{
        //    return GameBoard.GameGrid[position.XPos, position.YPos].PlacePiece(ComputerPieceStyle, BigBrain);
        //}
        private (int XPos, int YPos) FindBestMove()
        {
            (int XPos, int YPos) nextMovePosition = new();
            Dictionary<(int Xpos, int Ypos), PieceState> bestMoveGrid = new();

            for (int i = 0; i < SolutionBoard.GridSizeX; i++)
            {
                for (int j = 0; j < SolutionBoard.GridSizeY; j++)
                {
                    bestMoveGrid.Add(SolutionBoard.GameGrid[i, j].Position, SolutionBoard.GameGrid[i, j].PieceState);
                }
            }
            var gridX = SolutionBoard.GridSizeX;
            var gridY = SolutionBoard.GridSizeY;

            if (gridX < 5 && gridX % 2 == 1)
            {
                var centerPos = (XPos: gridX / 2, YPos: gridY / 2);
                if (bestMoveGrid[centerPos] == PieceState.NotPlaced)
                {
                    nextMovePosition = centerPos;
                    return centerPos;
                }
            }


            return (XPos: nextMovePosition.XPos, YPos: nextMovePosition.YPos);

            //var playerNeighbours = 0;
            //var computerNeighbours = 0;
            //List<(int, int)> dxdy = new()
            //{
            //    ( -1, -1 ), ( -1, 0 ), ( -1, 1 ),
            //    (  0, -1 ),            (  0, 1 ),
            //    (  1, -1 ), (  1, 0 ), (  1, 1 ),
            //};

            //foreach (var (dx, dy) in dxdy)
            //{
            //    var neighbourX = ((piece.Position.Item1 + dx + SolutionBoard.GridSizeX) % SolutionBoard.GridSizeX);
            //    var neighbourY = ((piece.Position.Item2 + dy + SolutionBoard.GridSizeY) % SolutionBoard.GridSizeY);
            //    var neighbourPiece = SolutionBoard.GameGrid[neighbourX, neighbourY];


            //    if (neighbourPiece.PieceState == PieceState.PlayerPlaced) playerNeighbours++;
            //    else if (neighbourPiece.PieceState == PieceState.ComputerPlaced) computerNeighbours++;
            //}

            //piece.UpdatePlayerNeighbours(playerNeighbours, computerNeighbours, BigBrain);
        }





    }
}
