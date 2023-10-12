using System.Text;
using TicTakToe.Logic;
namespace TicTakToe.Game
{

    public class Board
    {
        public int GridSizeX { get; private set; }
        public int GridSizeY { get; private set; }
        public Piece[,] GameGrid { get; private set; }
        public int WinCondition { get; private set; }
        public Board(int gridSizeX, int gridSizeY, int winCondition)
        {
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            WinCondition = winCondition;
            GameGrid = NewGame();
        }
        public Board(int gridSizeX, int gridSizeY, int winCondition, Piece[,] gameGrid)
        {
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            WinCondition = winCondition;
            GameGrid = gameGrid;
        }

        public Piece[,] NewGame()
        {
            var gameGridInitialization = new Piece[GridSizeX, GridSizeY];
            GameLogic.ForLoop(gameGridInitialization, (i, j, cell) =>
            {
                var piecePos = (X: i, Y: j);
                gameGridInitialization[i, j] = new Piece(piecePos);
            });

            return gameGridInitialization;
        }

        public void DrawBoard()
        {
            GameLogic.ForLoop(GameGrid, (i, j, cell) =>
            {
                Console.Write($"[{(cell.PieceStyle)}]{(j == GridSizeY - 1 ? "\n" : "")}");
            });
        }

        public Board CopyBoard()
        {
            Piece[,] copyGameGrid = new Piece[GridSizeX, GridSizeY];
            for (int i = 0; i < GridSizeX; i++)
            {
                for (int j = 0; j < GridSizeY; j++)
                {
                    copyGameGrid[i, j] = new Piece(i, j, GameGrid[i, j].PieceState, GameGrid[i, j].PieceStyle);
                }
            }

            return new Board(GridSizeX, GridSizeY, WinCondition, copyGameGrid);
        }
        public void drawingtheGameGrid()
        {
            Console.Clear();
            var maxCellValue = GridSizeX * GridSizeY;
            var maxCellValueLength = maxCellValue.ToString().Length;

            var validPlacementPositions = new SortedDictionary<int, (int, int)>();
            int counter = 0;
            var gridChoice = new StringBuilder();
            for (int i = 0; i < GridSizeX; i++)
            {
                for (int j = 0; j < GridSizeY; j++)
                {
                    if (GameGrid[i, j].PieceStyle == " " && GameGrid[i, j].PieceState == Logic.Enums.PieceState.NotPlaced)
                    {
                        counter++;
                        var counterAdjusted = counter.ToString().PadLeft(maxCellValueLength);
                        gridChoice.Append($"[ {counterAdjusted} ]");
                        validPlacementPositions.Add(counter, (i, j));
                    }
                    else
                    {
                        gridChoice.Append($"[ {GameGrid[i, j].PieceStyle.PadLeft(maxCellValueLength)} ]");
                    }
                    if (j == GridSizeX - 1)
                    {
                        gridChoice.Append(Environment.NewLine);
                    }
                }
            }

            Console.WriteLine(gridChoice.ToString());
        }


    }
}
