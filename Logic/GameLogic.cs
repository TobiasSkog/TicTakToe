using TicTakToe.ConsoleIO;
using TicTakToe.Game;
using TicTakToe.Logic.Computer;
using TicTakToe.Logic.Enums;

namespace TicTakToe.Logic
{
    //   "\n\tGames Played: " +
    //          "\n\tGames Won: " +
    //           "\n\tLeast amount of draws for one game: " +
    //           "\n\tMaximum amount of draws for one game: ");
    public class GameLogic
    {
        public GameStatistics Stats { get; set; }
        private int GameGridSize;
        private bool ComputerMoveNow;
        private string PlayerPiece;
        private string ComputerPiece;
        private Board GameBoard;
        public GameLogic()
        {
            Stats = new GameStatistics();
            InitializeGame();
            GameLoop();
        }
        public void InitializeGame()
        {
            PlayerPiece = UserInteraction.GetPlayerIcon("Would you like to play as '🗙' or '❍': ", "XO");
            Console.WriteLine();
            ComputerPiece = (PlayerPiece == "🗙") ? "❍" : "🗙";
            ComputerMoveNow = ComputerPiece == "❍" ? true : false;
            Stats.PlayerMoves = 0;
            Stats.ComputerMoves = 0;
            GameGridSize = UserInteraction.GetBoardSize();
            GameBoard = new Board(GameGridSize, GameGridSize, 3);
        }
        public bool EndOfGame((bool gameEnd, PlayerType winner) gameInfo)
        {
            if (gameInfo.gameEnd || !AnyRemainingMoves(GameBoard))
            {
                GameBoard.drawingtheGameGrid();
                if (gameInfo.winner != PlayerType.Invalid)
                {
                    if (gameInfo.winner == PlayerType.Player)
                    {
                        Stats.GamesWon++;
                        Stats.LowestMoveCount = Stats.LowestMoveCount < Stats.PlayerMoves ? Stats.LowestMoveCount : Stats.PlayerMoves;
                        Stats.HighestMoveCount = Stats.HighestMoveCount > Stats.PlayerMoves ? Stats.HighestMoveCount : Stats.PlayerMoves;
                        Console.WriteLine($"Player have won the game!");
                    }
                    else if (gameInfo.winner == PlayerType.Computer)
                    {
                        Console.WriteLine($"Computer have won the game!");
                    }
                    else
                    {
                        Stats.GamesDraw++;
                        GameBoard.DrawBoard();
                        Console.WriteLine("No more available moves!");
                    }
                }
                Stats.GamesPlayed++;
                var playAgain = UserInteraction.PlayAgain("Do you want to play again: ");
                if (playAgain)
                {
                    InitializeGame();
                    return false;
                }
            }
            return true;
        }

        public void GameLoop()
        {

            var pieceInformation = GetMove();

            UpdateGameState(pieceInformation);

            var gameInfo = GameOver(GameBoard);
            if (gameInfo.gameEnded && EndOfGame(gameInfo))
            {
                return;
            }
            else
            {
                GameLoop();
            }
        }
        public void UpdateGameState(Data data)
        {
            if (data.Pos.X > -1 || data.Pos.Y > -1)
            {
                GameBoard.GameGrid[data.Pos.X, data.Pos.Y].UpdatePiece(data.Piece == PlayerPiece ? PlayerType.Player : PlayerType.Computer, data.Piece);
            }
        }

        public static (bool gameEnded, PlayerType winner) GameOver(Board gameBoard)
        {
            var x = gameBoard.GridSizeX;
            var y = gameBoard.GridSizeY;
            var board = gameBoard.GameGrid;
            var win = gameBoard.WinCondition;
            var player = PlayerType.Player;
            var computer = PlayerType.Computer;

            for (int gridModifier = 0; gridModifier <= x - win; gridModifier++)
            {
                // Check X axis for 3 in a row
                for (int i = 0; i < x; i++)
                {
                    //Console.WriteLine($"({i},{gridModifier}): {board[i, gridModifier].PieceStyle}");
                    //Console.WriteLine($"({i},{gridModifier + 1}): {board[i, gridModifier].PieceStyle}");
                    //Console.WriteLine($"({i},{gridModifier + 2}): {board[i, gridModifier].PieceStyle}");
                    if (board[i, gridModifier].PieceState == board[i, gridModifier + 1].PieceState && board[i, gridModifier + 1].PieceState == board[i, gridModifier + 2].PieceState)
                    {
                        var state = board[i, gridModifier].PieceState;
                        if (state == PieceState.ComputerPlaced)
                        {
                            return (true, computer);
                        }
                        else if (state == PieceState.PlayerPlaced)
                        {
                            return (true, player);
                        }
                    }

                    if (board[gridModifier, gridModifier].PieceState == board[gridModifier + 1, gridModifier + 1].PieceState &&
                          board[gridModifier + 1, gridModifier + 1].PieceState == board[gridModifier + 2, gridModifier + 2].PieceState)
                    {

                        var state = board[gridModifier, gridModifier].PieceState;
                        if (state == PieceState.ComputerPlaced)
                        {
                            return (true, computer);
                        }
                        else if (state == PieceState.PlayerPlaced)
                        {
                            return (true, player);
                        }
                    }

                    if (board[gridModifier, gridModifier + 2].PieceState == board[gridModifier + 1, gridModifier + 1].PieceState &&
                        board[gridModifier + 1, gridModifier + 1].PieceState == board[gridModifier + 2, gridModifier].PieceState)
                    {

                        var state = board[gridModifier, gridModifier + 2].PieceState;
                        if (state == PieceState.ComputerPlaced)
                        {
                            return (true, computer);
                        }
                        else if (state == PieceState.PlayerPlaced)
                        {
                            return (true, player);
                        }
                    }
                }
                // Check Y axis for 3 in a row
                for (int j = 0; j < y; j++)
                {
                    if (board[gridModifier, j].PieceState == board[gridModifier + 1, j].PieceState && board[gridModifier + 1, j].PieceState == board[gridModifier + 2, j].PieceState)
                    {
                        var state = board[gridModifier, j].PieceState;
                        if (state == PieceState.ComputerPlaced)
                        {
                            return (true, computer);
                        }
                        else if (state == PieceState.PlayerPlaced)
                        {
                            return (true, player);
                        }
                    }
                }
            }

            if (!AnyRemainingMoves(gameBoard))
            {
                return (true, PlayerType.Draw);
            }
            return (false, PlayerType.Invalid);
        }


        public Data GetMove()
        {

            if (ComputerMoveNow)
            {
                Stats.ComputerMoves++;
                var pos = AI.FindBestMove(GameBoard, ComputerPiece, PlayerPiece);
                ComputerMoveNow ^= true;
                return new Data(pos, ComputerPiece);
            }
            else
            {
                Stats.PlayerMoves++;
                var pos = UserInteraction.GetPlayerMove("Enter your next move from one of\nthe available numbers on the grid: ", GameBoard);
                ComputerMoveNow ^= true;
                return new Data(pos, PlayerPiece);
            }
        }

        public static bool AnyRemainingMoves(Board gameBoard)
        {
            foreach (var cell in gameBoard.GameGrid)
            {
                if (cell.PieceState == PieceState.NotPlaced)
                {
                    return true;
                }
            }
            return false;
        }
        public static int log2(int n)
        {
            return (n == 1) ? 0 : 1 + log2(n / 2);
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
