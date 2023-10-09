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
                var piecePos = new Tuple<int, int>(i, j);
                gameGridInitialization[i, j] = new Piece(piecePos);
            });

            return gameGridInitialization;
        }

        public void DrawBoard()
        {
            GameLogic.ForLoop(GameGrid, (i, j, cell) =>
            {
                Console.WriteLine($"[{(cell.PieceStyle)}{(i == GridSizeX - 1 ? "\n" : "")}]");
            });
        }


    }
}
