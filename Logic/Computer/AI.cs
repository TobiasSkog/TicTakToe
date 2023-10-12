using TicTakToe.Game;
using TicTakToe.Logic.Enums;

namespace TicTakToe.Logic.Computer
{
    public static class AI
    {
        private const int MIN = -1000;
        private const int MAX = 1000;



        public static (int X, int Y) FindBestMove(Board gameBoard, string computerPiece, string playerPiece)
        {
            var bestValue = MIN;
            (int X, int Y) bestMove = new(-1, -1);
            var x = gameBoard.GridSizeX;
            var y = gameBoard.GridSizeY;
            var copyBoard = gameBoard.CopyBoard();
            var copyGameGrid = copyBoard.GameGrid;
            int maxDepth;
            if (x * y > 30)
            {
                maxDepth = GameLogic.log2(x * y);
            }
            else
            {
                maxDepth = 350;
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var state = copyGameGrid[i, j].PieceState;

                    if (state == PieceState.NotPlaced)
                    {
                        copyGameGrid[i, j].AIEvaluation(PlayerType.Computer, computerPiece);

                        var moveValue = Minimax(copyBoard, 0, maxDepth, true, MIN, MAX, playerPiece, computerPiece);

                        copyGameGrid[i, j].ResetPiece();

                        if (moveValue > bestValue)
                        {
                            bestMove = (i, j);
                            bestValue = moveValue;
                        }
                    }
                }
            }
            return bestMove;
        }

        private static int Minimax(Board gameBoard, int depth, int maxDepth, bool isMaximizingPlayer, int alpha, int beta, string computerPiece, string playerPiece)
        {
            var score = Evaluate(gameBoard, depth);
            if (depth >= maxDepth) //score != 0 || 
            {
                return score;
            }
            if (!GameLogic.AnyRemainingMoves(gameBoard))
            {
                return 0;
            }

            if (isMaximizingPlayer)
            {
                var best = MIN;


                for (int i = 0; i < gameBoard.GridSizeX; i++)
                {
                    for (int j = 0; j < gameBoard.GridSizeY; j++)
                    {
                        if (gameBoard.GameGrid[i, j].PieceState == PieceState.NotPlaced)
                        {
                            gameBoard.GameGrid[i, j].AIEvaluation(PlayerType.Computer, computerPiece);
                            var val = Minimax(gameBoard, depth + 1, maxDepth, false, alpha, beta, computerPiece, playerPiece);
                            gameBoard.GameGrid[i, j].ResetPiece();
                            best = Math.Max(best, val);
                            alpha = Math.Max(alpha, best);
                        }
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
                return best;
            }
            else
            {
                var best = MAX;
                for (int i = 0; i < gameBoard.GridSizeX; i++)
                {
                    for (int j = 0; j < gameBoard.GridSizeY; j++)
                    {
                        if (gameBoard.GameGrid[i, j].PieceState == PieceState.NotPlaced)
                        {
                            gameBoard.GameGrid[i, j].AIEvaluation(PlayerType.Player, playerPiece);
                            var val = Minimax(gameBoard, depth + 1, maxDepth, true, alpha, beta, computerPiece, playerPiece);
                            gameBoard.GameGrid[i, j].ResetPiece();
                            best = Math.Min(best, val);
                            beta = Math.Min(beta, best);
                        }
                        if (beta <= alpha)
                        {
                            break;
                        }
                    }
                }
                return best;
            }
        }

        private static int Evaluate(Board gameBoard, int depth)
        {
            // adding a grid modifier for our checks that should accommodate the size of our gameboard and be able
            // to look for a win condition on any given sized gameBoard
            var board = gameBoard.GameGrid;
            var x = gameBoard.GridSizeX;
            var y = gameBoard.GridSizeY;
            var win = gameBoard.WinCondition;

            for (int gridModifier = 0; gridModifier <= x - win; gridModifier++)
            {
                // Check X axis for 3 in a row
                for (int i = 0; i < x; i++)
                {
                    if (board[i, gridModifier].PieceState == board[i, gridModifier + 1].PieceState && board[i, gridModifier + 1].PieceState == board[i, gridModifier + 2].PieceState)
                    {
                        if (board[i, gridModifier].PieceState == PieceState.ComputerPlaced)
                        {
                            return +10 - depth;
                        }
                        else if (board[i, gridModifier].PieceState == PieceState.PlayerPlaced)
                        {
                            return -10 + depth;
                        }
                    }

                    // Check Diagonally 
                    // 0,0   2,0
                    //    1,1
                    // 0,2    2,2
                    // gridModifier = 0 ... GridSizeX - WinCondition >>>> GridSizeX = 3, WinCondition = 3, gridModifier = 0,0
                    // gridModifier min = 0, max = 0
                    // diagonal = (0,0), (1,1), (2,2) | (0,2), (1,1), (2,0)
                    // (gridModifier, gridModifier), (gridModifier + 1, gridModifier + 1), (gridModifier + 2, gridModifier + 2)
                    // (gridModifier, gridModifier + 2), (gridModifier + 1, gridModifier + 1), (gridModifier + 2, gridModifier)

                    if (board[gridModifier, gridModifier].PieceState == board[gridModifier + 1, gridModifier + 1].PieceState &&
                          board[gridModifier + 1, gridModifier + 1].PieceState == board[gridModifier + 2, gridModifier + 2].PieceState)
                    {
                        if (board[gridModifier, gridModifier].PieceState == PieceState.ComputerPlaced)
                        {
                            return +10 - depth;
                        }
                        else if (board[gridModifier, gridModifier].PieceState == PieceState.PlayerPlaced)
                        {
                            return -10 + depth;
                        }
                    }

                    if (board[gridModifier, gridModifier + 2].PieceState == board[gridModifier + 1, gridModifier + 1].PieceState &&
                        board[gridModifier + 1, gridModifier + 1].PieceState == board[gridModifier + 2, gridModifier].PieceState)
                    {
                        if (board[gridModifier, gridModifier].PieceState == PieceState.ComputerPlaced)
                        {
                            return +10 - depth;
                        }
                        else if (board[gridModifier, gridModifier].PieceState == PieceState.PlayerPlaced)
                        {
                            return -10 + depth;
                        }
                    }

                }
                for (int j = 0; j < y; j++)
                {
                    if (board[j, gridModifier].PieceState == board[j, gridModifier + 1].PieceState && board[j, gridModifier + 1].PieceState == board[j, gridModifier + 2].PieceState)
                    {
                        if (board[j, gridModifier].PieceState == PieceState.ComputerPlaced)
                        {
                            return +10 - depth;
                        }
                        else if (board[j, gridModifier].PieceState == PieceState.PlayerPlaced)
                        {
                            return -10 + depth;
                        }
                    }
                }
            }
            return 0;
        }
    }
}
//GameLogic.ForLoop(gameBoard.GameGrid, (i, j, piece) =>
//{
//    var state = piece.PieceState;
//    if (state == PieceState.NotPlaced)
//    {
//        piece.AIEvaluation(PlayerType.Computer, computerPiece);
//        var val = Minimax(gameBoard, depth + 1, false, alpha, beta, computerPiece, playerPiece);
//        best = Math.Max(best, val) - depth;
//        alpha = Math.Max(alpha, best) - depth;
//    }
//    if (beta <= alpha)
//    {
//        return;
//    }
//});
//return best;


//GameLogic.ForLoop(copyGameGrid, (i, j, piece) =>
//{
//    var state = piece.PieceState;

//    if (state == PieceState.NotPlaced)
//    {
//        piece.AIEvaluation(PlayerType.Computer, computerPiece);

//        var moveValue = Minimax(gameBoard, 0, true, 0, 0, playerPiece, computerPiece);

//        if (moveValue > bestValue)
//        {
//            bestMove = (i, j);
//            bestValue = moveValue;
//        }
//    }
//});
//return bestMove;