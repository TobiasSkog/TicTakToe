using TicTakToe.Logic;
namespace TicTakToe.Game
{

    internal class Board
    {
        public int GridSizeX { get; private set; }
        public int GridSizeY { get; private set; }
        public Piece[,] GameGrid { get; private set; }
        public Board(int gridSizeX, int gridSizeY)
        {
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            GameGrid = NewGame();
        }

        public Piece[,] NewGame()
        {
            var gameGridInitialization = new Piece[GridSizeX, GridSizeY];
            GameLogic.ForLoop(gameGridInitialization, (i, j, cell) =>
            {
                var piecePos = (XPos: i, YPos: j);
                gameGridInitialization[i, j] = new Piece(piecePos);
            });

            return gameGridInitialization;
        }




    }
}
