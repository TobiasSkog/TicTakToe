namespace TicTakToe.Game
{

    internal class Board
    {
        public int GridSizeX { get; private set; }
        public int GridSizeY { get; private set; }
        private char PlayerPiece { get; set; }
        private char ComputerPiece { get; set; }
        public Piece[,] BoardArray { get; private set; }
        public Board(int gridSizeX, int gridSizeY, char playerPiece)
        {
            PlayerPiece = playerPiece;

            ComputerPiece = (PlayerPiece == 'X') ? 'O' : 'X';


            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
            InitializeGameBoard();
        }

        private Piece[,] InitializeGameBoard()
        {
            var boardArray = new Piece[GridSizeX, GridSizeY];
            for (int i = 0; i < GridSizeX; i++)
            {
                for (int j = 0; j < GridSizeY; j++)
                {
                    boardArray[i, j] = new Piece(new(i, j));
                }
            }

            return boardArray;
        }
    }
}
